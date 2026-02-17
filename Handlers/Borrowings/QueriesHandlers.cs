using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using System.ComponentModel.DataAnnotations;
using static LibraryManagement.CQRS.Query.BorrowingQueries;

namespace LibraryManagement.query
{
    public class BorrowingQueryHandlers
    {
        // Get All Borrowings
        public class GetAllBorrowingsHandler : IRequestHandler<GetAllBorrowingsQuery, List<BorrowingModel>>
        {
            private readonly IBorrowingRepository _repo;
            public GetAllBorrowingsHandler(IBorrowingRepository repo) { _repo = repo; }
            public async Task<List<BorrowingModel>> Handle(GetAllBorrowingsQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetAllBorrowingsAsync();
            }
        }

        // Get Borrowing by Id
        public class GetBorrowingByIdHandler : IRequestHandler<GetBorrowingByIdQuery, BorrowingModel>
        {
            private readonly IBorrowingRepository _repo;
            public GetBorrowingByIdHandler(IBorrowingRepository repo) { _repo = repo; }
            public async Task<BorrowingModel> Handle(GetBorrowingByIdQuery request, CancellationToken cancellationToken)
            {
                var borrowing = await _repo.GetBorrowingByIdAsync(request.Id);
                if (borrowing == null) throw new ValidationException("Borrowing not found.");
                return borrowing;
            }
        }

        // Get Borrowings by UserId
        public class GetBorrowingsByUserIdHandler : IRequestHandler<GetBorrowingsByUserIdQuery, List<BorrowingModel>>
        {
            private readonly IBorrowingRepository _repo;
            public GetBorrowingsByUserIdHandler(IBorrowingRepository repo) { _repo = repo; }
            public async Task<List<BorrowingModel>> Handle(GetBorrowingsByUserIdQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetBorrowingsByUserIdAsync(request.UserId);
            }
        }

        // Get Active Borrowings
        public class GetActiveBorrowingsHandler : IRequestHandler<GetActiveBorrowingsQuery, List<BorrowingModel>>
        {
            private readonly IBorrowingRepository _repo;
            public GetActiveBorrowingsHandler(IBorrowingRepository repo) { _repo = repo; }
            public async Task<List<BorrowingModel>> Handle(GetActiveBorrowingsQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetActiveBorrowingsAsync();
            }
        }
    }
}
