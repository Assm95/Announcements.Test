using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Announcements.Test.Application.Common.Exceptions;
using Announcements.Test.Application.DTO;
using Announcements.Test.Application.Interfaces;
using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Entities;
using Announcements.Test.Shared;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Announcements.Test.Application.Features.Announcements.Queries
{
    public class GetAnnouncementsQuery : IRequest<Result<List<AnnouncementDto>>>
    {
        #region Sorting

        public SortAnnouncementsDto? Sort { get; set; }

        #endregion

        #region Filtering

        public FilterAnnouncementsDto? Filter { get; set; }

        #endregion

        #region Search

        public string? SearchString { get; set; }


        #endregion

        #region Pagination

        public PaginationDto? Pagination { get; set; }

        #endregion

        public bool IncludeImageData { get; set; }
    }

    internal class GetAnnouncementsQueryHandler : IRequestHandler<GetAnnouncementsQuery, Result<List<AnnouncementDto>>>
    {
        private readonly IUnitOfWork _announcementsUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;

        public GetAnnouncementsQueryHandler(IUnitOfWork announcementsUnitOfWork, IMapper mapper, IFileStorage fileStorage)
        {
            _announcementsUnitOfWork = announcementsUnitOfWork;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async Task<Result<List<AnnouncementDto>>> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            return await ExceptionWrapper<Result<List<AnnouncementDto>>>.Catch(async () =>
            {
                var query = _announcementsUnitOfWork.Repository<Announcement>().Entities;
                query = query.Include(x => x.User);
                    

                if (!string.IsNullOrWhiteSpace(request.SearchString))
                    query = SearchBy(query, request.SearchString);

                if (request.Filter != null)
                    query = FilterBy(query, request.Filter);

                if (request.Pagination != null)
                    query = GetPagination(query, request.Pagination);

                if(request.Sort != null)
                    query = SortBy(query, request.Sort);

                var announcementsList = await query.ToListAsync(cancellationToken);

                List<AnnouncementDto> listAnnouncementsDto = _mapper.Map<List<AnnouncementDto>>(announcementsList);

                if (request.IncludeImageData)
                {
                    foreach (var dto in listAnnouncementsDto)
                    {
                        dto.Image.FileData = await _fileStorage.GetFileDataAsync(dto.Image.FileName) ??
                                             throw new NotFoundException("File not found");
                    }
                }
                
                return await Task.FromResult(new Result<List<AnnouncementDto>>(listAnnouncementsDto));
            });
        }


        private static IQueryable<Announcement> FilterBy(IQueryable<Announcement> query, FilterAnnouncementsDto filter)
        {
            if (filter.CreatedAt != null)
            {
                var createdAt = filter.CreatedAt;

                if (createdAt.Start.HasValue)
                    query = query.Where(x => x.CreatedAt >= createdAt.Start);

                if(createdAt.End.HasValue)
                    query = query.Where(x => x.CreatedAt < createdAt.End.Value.AddDays(1));
            }

            if (filter.ExpirationDate != null)
            {
                var expirationDate = filter.ExpirationDate;

                if (expirationDate.Start.HasValue)
                    query = query.Where(x => x.ExpirationDate >= expirationDate.Start);

                if (expirationDate.End.HasValue)
                    query = query.Where(x => x.ExpirationDate < expirationDate.End.Value.AddDays(1));
            }

            if (filter.Number.HasValue)
                query = query.Where(x => x.Number == filter.Number);

            if (filter.Rating.HasValue)
                query = query.Where(x => x.Rating == filter.Rating);

            if (filter.UserId.HasValue)
                query = query.Where(x => x.UserId == filter.UserId);

            return query;
        }

        private static IQueryable<Announcement> GetPagination(IQueryable<Announcement> query, PaginationDto pagination)
        {
            return query.Skip(pagination.PageNumber)
                .Take(pagination.PageSize);
        }

        private static IQueryable<Announcement> SearchBy(IQueryable<Announcement> query, string searchString)
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
            
            return query;
        }

        private static IQueryable<Announcement> SortBy(IQueryable<Announcement> query, SortAnnouncementsDto sort)
        {
            var keySelector = GetSortProperty(sort);
            return sort.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        }

        private static Expression<Func<Announcement, object>> GetSortProperty(SortAnnouncementsDto sort)
        {
            return sort.FieldName?.ToLower() switch
            {
                "number" => x => x.Number,
                "createdat" => x => x.CreatedAt,
                "rating" => x => x.Rating,
                "expirationdate" => x => x.ExpirationDate,
                _ => x => x.Id,
            };
        }
    }
}
