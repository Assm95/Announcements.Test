using Announcements.EF.Exceptions;
using Announcements.EF.Filters;
using Announcements.EF.Models;
using Announcements.EF.Resources;
using Announcements.EF.Utils;
using DelegateDecompiler;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Announcements.EF.Const;
using DelegateDecompiler.EntityFrameworkCore;
using File = Announcements.EF.Models.File;

namespace Announcements.EF.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly AnnouncementDbContext _dbContext;

        public AnnouncementService(AnnouncementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  async Task<Announcement> CreateAsync(User user, int number, string text, File image, int rating, DateTime expirationDate,
            CancellationToken cancellationToken)
        {
            Announcement? announcement =
                await _dbContext.Announcements.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Number == number,
                    cancellationToken: cancellationToken);

            if (announcement != null) 
                throw new DomainException(ErrorsSource.AnnouncementExist);

            announcement = new Announcement(user, number, text, image, rating, expirationDate);
            _dbContext.Announcements.Add(announcement);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return announcement;
        }

        public async Task UpdateAsync(Guid id, User user, int number, string text, File image,  int rating, DateTime expirationDate,
            CancellationToken cancellationToken)
        {
            Announcement announcement = await GetAsync(id, cancellationToken);

            if (!user.IsAdmin && announcement.UserId != user.Id)
                throw new DomainAccessDeniedException(ErrorsSource.AnnouncementOwnerInvalid);

            if (announcement.Number != number)
            {
                bool numberExist = await FindAnnouncementAsync(number, cancellationToken) != null;

                if (numberExist)
                    throw new DomainException(ErrorsSource.AnnouncementNumberUnique);
            }


            announcement.Number = number;
            announcement.Text = text;
            announcement.Image = image;
            announcement.Rating = rating;
            announcement.ExpirationDate = expirationDate;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(Guid id, User user, CancellationToken cancellationToken)
        {
            Announcement announcement = await GetAsync(id, cancellationToken);

            if (!user.IsAdmin && announcement.UserId != user.Id)
                throw new DomainAccessDeniedException(ErrorsSource.AnnouncementOwnerInvalid);

            _dbContext.Announcements.Remove(announcement);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Announcement> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            Announcement? announcement = await FindAnnouncementAsync(id, cancellationToken);

            if (announcement == null)
                throw new DomainNotFoundException("Announcement");

            return announcement;
        }

        public async Task<CustomCollection<Announcement>> GetAllAsync(PaginationFilter? pagination,
            AnnouncementsFilter? filter, string? searchString, SortingFilter? sorting,
            CancellationToken cancellationToken)
        {
            searchString = searchString?.Trim().ToLower();

            if (pagination != null)
            {
                pagination.PageStart = Math.Max(pagination.PageStart, 0);
                pagination.PageCount = Math.Max(pagination.PageCount, 0);
            }

            IQueryable<Announcement> query = _dbContext.Announcements
                .Include(x => x.User);

            //search
            if (!string.IsNullOrWhiteSpace(searchString))
            {

                if (DateTime.TryParse(searchString, out DateTime dateTime))
                {
                    query = query.Where(x => x.CreatedAt == dateTime ||
                                             x.ExpirationDate == dateTime);
                }
                else if (int.TryParse(searchString, out int intValue))
                {
                    query = query.Where(x => x.Number == intValue ||
                                             x.Rating == intValue);
                }
                else
                    query = query.Where(x => x.User.Name.ToLower().Contains(searchString));
            }

            //filter
            if (filter?.UserId != null)
                query = query.Where(x => x.UserId == filter.UserId);

            DateTime? createdAtStart = GetDateTime(filter?.CreatedAtStart);
            DateTime? createdAtEnd = GetDateTime(filter?.CreatedAtEnd)?.AddDays(1);

            DateTime? expirationDateStart = GetDateTime(filter?.ExpirationDateStart);
            DateTime? expirationDateEnd = GetDateTime(filter?.ExpirationDateEnd)?.AddDays(1);

            if (createdAtStart.HasValue)
                query = query.Where(x => x.CreatedAt >= createdAtStart);

            if (createdAtEnd.HasValue)
                query = query.Where(x => x.CreatedAt < createdAtEnd);

            if (expirationDateStart.HasValue)
                query = query.Where(x => x.ExpirationDate >= expirationDateStart);

            if (expirationDateEnd.HasValue)
                query = query.Where(x => x.ExpirationDate < expirationDateEnd);

            if (filter?.Number != null)
                query = query.Where(x => x.Number == filter.Number);

            if (filter?.Rating != null)
                query = query.Where(x => x.Rating == filter.Rating);

            long totalAnnouncement = await query.LongCountAsync(cancellationToken);

            //pagination
            if(pagination?.PageStart != null)
                query = query.Skip(pagination.PageStart);

            if(pagination?.PageCount != null)
                query = query.Take(pagination.PageCount);

            //sort
            if (sorting != null)
            {
                var keySelector = GetSortProperty(sorting);
                query = sorting.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
            }

            var announcements = await query
                .DecompileAsync()
                .ToListAsync(cancellationToken);

            return new CustomCollection<Announcement>
            {
                Items = announcements,
                TotalCount = totalAnnouncement
            };
        }

        private async Task<Announcement?> FindAnnouncementAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Announcements
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }


        private async Task<Announcement?> FindAnnouncementAsync(int number, CancellationToken cancellationToken) =>
            await _dbContext.Announcements.FirstOrDefaultAsync(x => x.Number == number, cancellationToken);

        private Expression<Func<Announcement, object>> GetSortProperty(SortingFilter sorting)
        {
            return sorting.ColumnName?.ToLower() switch
            {
                "number" => x => x.Number,
                "createdAt" => x => x.CreatedAt,
                "rating" => x => x.Rating,
                "expirationDate" => x => x.ExpirationDate,
                _ => x => x.Id,
            };
        }

        private DateTime? GetDateTime(string? value)
        {
            if(string.IsNullOrEmpty(value)) 
                return null;

            if (!DateTime.TryParseExact(value, FormatConst.DateTimeFormat, null, DateTimeStyles.None, out DateTime dateTime))
                throw new DomainException(ErrorsSource.DateTimeInvalidFormat(value));

            return dateTime;
        }
    }
}
