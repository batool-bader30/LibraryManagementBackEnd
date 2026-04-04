using LibraryManagement.DTO;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using MediatR;
using LibraryManagement.command;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.CQRS.Handlers
{

    public class UpdateUserCommandHandler : IRequestHandler<UserCommands.UpdateUserCommand, string>
    {
        private readonly IUserRepository _repo;

        public UpdateUserCommandHandler(IUserRepository repo) => _repo = repo;

        public async Task<string> Handle(UserCommands.UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.User.UserName))
                return "Username is required";

            // 1. تجهيز موديول المستخدم بالبيانات الأساسية
            var user = new UserModel
            {
                Id = request.Id,
                UserName = request.User.UserName,
                Email = request.User.Email,
                PhoneNumber = request.User.PhoneNumber // إضافة الهاتف هنا
            };

            // 2. إرسال المستخدم مع كلمة المرور الجديدة للميثود المعدلة في الـ Repository
            // لاحظي أننا نمرر request.User.Password كباراميتر ثانٍ
            return await _repo.UpdateUserAsync(user, request.User.Password);
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<UserCommands.DeleteUserCommand, bool>
    {
        private readonly IUserRepository _repo;

        public DeleteUserCommandHandler(IUserRepository repo) => _repo = repo;

        public async Task<bool> Handle(UserCommands.DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteUserAsync(request.Id);
        }
    }
}
