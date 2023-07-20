using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Announcements.Test.Application.DTO;
using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Domain.Entities;
using Announcements.Test.Shared;
using MediatR;

namespace Announcements.Test.Application.Features.Announcements.Queries
{
    public class GetAnnouncementsQuery : IRequest<Result<List<AnnouncementDto>>>
    {
        #region Sorting

        public string? FieldName { get; set; }

        public string? SortOrder { get; set; }

        #endregion

        #region Filtering

        public FilterDto? Filter { get; set; }

        #endregion

        #region Search

        public string? SearchString { get; set; }


        #endregion

        #region Pagination

        public PaginationDto? Pagination { get; set; }

        #endregion
    }

    internal class GetAnnouncementsQueryHandler : IRequestHandler<GetAnnouncementsQuery, Result<List<AnnouncementDto>>>
    {
        private readonly IUnitOfWork _announcementsUnitOfWork;

        public GetAnnouncementsQueryHandler(IUnitOfWork announcementsUnitOfWork)
        {
            _announcementsUnitOfWork = announcementsUnitOfWork;
        }

        public async Task<Result<List<AnnouncementDto>>> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            var query = _announcementsUnitOfWork.Repository<Announcement>().Entities;

            if (!string.IsNullOrWhiteSpace(request.SearchString))
                query = SearchBy(query, request.SearchString);

            if (request.Filter != null)
                query = FilterBy(query, request.Filter);

            if (request.Pagination != null)
                query = GetPagination(query, request.Pagination);

            //TODO маппинг в DTO
            //var announcements = await query
            //    .DecompileAsync()
            //    .ToListAsync(cancellationToken);

            List<AnnouncementDto> list = new List<AnnouncementDto>();

            return await Result<List<AnnouncementDto>>.SuccessAsync(list);
        }


        private static IQueryable<Announcement> FilterBy(IQueryable<Announcement> query, FilterDto filter)
        {
            if (filter.CreatedAt != null)
            {
                var createdAt = filter.CreatedAt;
                query = query.Where(x => x.CreatedAt >= createdAt.Start && x.CreatedAt < createdAt.End.AddDays(1));
            }

            if (filter.ExpirationDate != null)
            {
                var expirationDate = filter.ExpirationDate;
                query = query.Where(x => x.CreatedAt >= expirationDate.Start && x.CreatedAt < expirationDate.End.AddDays(1));
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
    }
}
