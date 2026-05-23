using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Topic.Contracts;
using Topic.Data;
using Topic.Entities;
using Topic.Models;
using Topic.Models.Identity;
using Topic.Service.Exceptions;

namespace Topic.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IdentityUserRole<User, IdentityRole> _userRoleManager;
        //private readonly ModelBuilder _modelBuilder;
        private readonly ICommentRepository _commentRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private const string _adminRole = "Admin";
        private const string _customerRole = "Customer";

        public AuthService(ApplicationDbContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator,
            IHttpContextAccessor httpContextAccessor, ICommentRepository commentRepository, ITopicRepository topicRepository /*,  ModelBuilder modelBuilder */)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _commentRepository = commentRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _httpContextAccessor = httpContextAccessor;
            _mapper = MappingInitializer.Initialize();
            _topicRepository = topicRepository;
            //_modelBuilder = modelBuilder;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == loginRequestDTO.Email.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDTO()
                {
                    User = null,
                    Token = string.Empty
                };
            }
            if (user.LockoutEnabled == true)
            {
                throw new UserIsBlockedException();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDTO userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDTO result = new LoginResponseDTO()
            {
                User = userDto,
                Token = token
            };

            return result;
        }

        public async Task Register(RegistrationRequestDTO registrationRequestDTO)
        {
            User user = new()
            {
                UserName = registrationRequestDTO.Email,
                NormalizedUserName = registrationRequestDTO.Email.ToUpper(),
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                PhoneNumber = registrationRequestDTO.PhoneNumber
            };


            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);

                if (result.Succeeded)
                {
                    var userToReturn = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == registrationRequestDTO.Email.ToLower());

                    if (userToReturn != null)
                    {
                        if (!await _roleManager.RoleExistsAsync(_customerRole))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(_customerRole));
                        }

                        UserDTO userDto = new()
                        {
                            Email = userToReturn.Email,
                            Id = userToReturn.Id,
                            PhoneNumber = userToReturn.PhoneNumber
                        };
                    }
                }
                else
                {
                    throw new RegistrationFailureException(result.Errors.FirstOrDefault().Description);
                }
                if(user != null)
                {
                    var res = await _userManager.AddToRoleAsync(user, _customerRole);
                }

                var rawUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                rawUser.LockoutEnabled = false;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RegisterAdmin(RegistrationRequestDTO registrationRequestDTO)
        {

            PasswordHasher<User> hasher = new();
            User user = new()
            {
                UserName = registrationRequestDTO.Email,
                NormalizedUserName = registrationRequestDTO.Email.ToUpper(),
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                PhoneNumber = registrationRequestDTO.PhoneNumber,
                PasswordHash = hasher.HashPassword(null, registrationRequestDTO.Password)
            };

            try
            {
                IdentityResult result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    var userToReturn = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == registrationRequestDTO.Email.ToLower());

                    if (userToReturn != null)
                    {
                        if (!await _roleManager.RoleExistsAsync(_adminRole))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(_adminRole));
                        }

                        UserDTO userDto = new()
                        {
                            Email = userToReturn.Email,
                            Id = userToReturn.Id,
                            PhoneNumber = userToReturn.PhoneNumber
                        };
                    }
                }
                else
                {
                    throw new RegistrationFailureException(result.Errors.FirstOrDefault().Description);
                }

                if (user != null)
                {
                    var res = await _userManager.AddToRoleAsync(user, _adminRole);
                }

                //PasswordHasher<User> hasher = new();

                var rawUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                rawUser.LockoutEnabled = false;
                //user.PasswordHash = hasher.HashPassword(null, user.);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<User> BlockUser(UserBlock_UnblockDTO userBlock_Unblock)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userBlock_Unblock.Id);
                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                user.LockoutEnabled = true;
                
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> UnblockUser(UserBlock_UnblockDTO userBlock_UnblockDTO)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userBlock_UnblockDTO.Id);
                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                user.LockoutEnabled = false;

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<UserInfo>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                throw new UserNotFoundException();
            }

            List<UserInfo> result = new();
            //List<User> users = new();

            if (AuthenticatedUserRole() == "Admin")
            {
                //List<TopicForGetingDTO> result = _mapper.Map<List<TopicForGetingDTO>>(topics);
                result = _mapper.Map<List<UserInfo>>(users);
            }
            else { throw new UnauthorizedAccessException(); }


            List<Comments> comments = new();
            //Comments[] commentsArray = new()
            for (int i = 0; i < result.Count; i++)
            {

                int commentCount = 0;
                comments = await _commentRepository.GetAllComments(x => x.UserId == result[i].Id);
                for (int j = 0; j < comments.Count; j++)
                {
                    commentCount++;
                }
                result[i].Comments = commentCount;
            }

            #region Old code
            //foreach (var user in result)
            //{
            //    //var commentList = 
            //    comments = await _context.Comments.Where(x => x.UserId == user.Id).ToListAsync();
            //    for (int i = 0; i < comments.Count; i++)
            //    {
            //        commentsArray[i] = comments[i];
            //    }
            //}

            //List<Comments> tempComments = new();
            //for (int i = 0; i < result.Count; i++)
            //{
            //    tempComments = await _context.Comments.Where(x => x.UserId == result[i].Id).ToListAsync();
            //    for (int j = 0; j < tempComments.Count; j++)
            //    {
            //        comments.Add(tempComments[j]);
            //    }
            //}

            //List<Comments> tempComments = new();
            //for (int i = 0; i < result.Count; i++)
            //{
            //    tempComments = await _context.Comments.Where(x => x.UserId == result[i].Id).ToListAsync();
            //    for (int j = 0; j < tempComments.Count; j++)
            //    {
            //        Comments comment = tempComments[j];
            //    }
            //}
            //for (int i = 0; i < comments.Count; i++)
            //{
            //    commentCount++;
            //}

            //int topicCount = 0;
            //foreach (var user in result)
            //{
            //    topics = await _context.Topics.Where(x => x.UserId == user.Id).ToListAsync();
            //}
            //for (int i = 0; i < topics.Count; i++)
            //{
            //    topicCount++;
            //}
            #endregion 

            List<TopicEntity> topics = new();
            for (int i = 0; i < result.Count; i++)
            {
                int topicCount = 0;
                topics = await _topicRepository.GetAllTopicsAsync(x => x.UserId == result[i].Id);
                for (int j = 0; j < topics.Count; j++)
                {
                    topicCount++;
                }
                result[i].Topics = topicCount;
            }

            

            for (int i = 0; i < result.Count; i++)
            {
                var roles = await _userManager.GetRolesAsync(users[i]);
                result[i].Role = roles[0];
                //result[i].Comments = commentCount;
                //result[i].Topics = topicCount;
            }

            

            return result;
        }

        public async Task<UserInfo> GetUsersByMail(string mail)
        {
            if (mail == null)
            {
                throw new UserNotFoundException();
            }

            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                throw new UserNotFoundException();
            }

            List<UserInfo> result = new();
            //List<User> users = new();

            if (AuthenticatedUserRole() == "Admin" || AuthenticatedUserRole() == "Customer")
            {
                //List<TopicForGetingDTO> result = _mapper.Map<List<TopicForGetingDTO>>(topics);
                result = _mapper.Map<List<UserInfo>>(users);
            }
            else { throw new UnauthorizedAccessException(); }


            List<Comments> comments = new();
            //Comments[] commentsArray = new()
            for (int i = 0; i < result.Count; i++)
            {

                int commentCount = 0;
                comments = await _commentRepository.GetAllComments(x => x.UserId == result[i].Id);
                for (int j = 0; j < comments.Count; j++)
                {
                    commentCount++;
                }
                result[i].Comments = commentCount;
            }

            #region Old code
            //foreach (var user in result)
            //{
            //    //var commentList = 
            //    comments = await _context.Comments.Where(x => x.UserId == user.Id).ToListAsync();
            //    for (int i = 0; i < comments.Count; i++)
            //    {
            //        commentsArray[i] = comments[i];
            //    }
            //}

            //List<Comments> tempComments = new();
            //for (int i = 0; i < result.Count; i++)
            //{
            //    tempComments = await _context.Comments.Where(x => x.UserId == result[i].Id).ToListAsync();
            //    for (int j = 0; j < tempComments.Count; j++)
            //    {
            //        comments.Add(tempComments[j]);
            //    }
            //}

            //List<Comments> tempComments = new();
            //for (int i = 0; i < result.Count; i++)
            //{
            //    tempComments = await _context.Comments.Where(x => x.UserId == result[i].Id).ToListAsync();
            //    for (int j = 0; j < tempComments.Count; j++)
            //    {
            //        Comments comment = tempComments[j];
            //    }
            //}
            //for (int i = 0; i < comments.Count; i++)
            //{
            //    commentCount++;
            //}

            //int topicCount = 0;
            //foreach (var user in result)
            //{
            //    topics = await _context.Topics.Where(x => x.UserId == user.Id).ToListAsync();
            //}
            //for (int i = 0; i < topics.Count; i++)
            //{
            //    topicCount++;
            //}
            #endregion 

            List<TopicEntity> topics = new();
            for (int i = 0; i < result.Count; i++)
            {
                int topicCount = 0;
                topics = await _topicRepository.GetAllTopicsAsync(x => x.UserId == result[i].Id);
                for (int j = 0; j < topics.Count; j++)
                {
                    topicCount++;
                }
                result[i].Topics = topicCount;
            }



            for (int i = 0; i < result.Count; i++)
            {
                var roles = await _userManager.GetRolesAsync(users[i]);
                result[i].Role = roles[0];
                //result[i].Comments = commentCount;
                //result[i].Topics = topicCount;
            }

            UserInfo userToReturn = new();

            foreach (var user in result)
            {
                if (user.Email == mail)
                {
                    userToReturn = user;
                }
            }

            if (userToReturn == null)
            {
                throw new UserNotFoundException();
            }

            return userToReturn;
        }
        public async Task<User> UpdateUser(UserUpdate user)
        {
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var userToUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (userToUpdate == null)
            {
                throw new UserNotFoundException();
            }


            userToUpdate.UserName = user.UserName;
            if (user.UserName != null)
            {
                userToUpdate.NormalizedUserName = user.UserName.ToUpper();
            }
            else
            {
                userToUpdate.NormalizedUserName = "";
            }
            userToUpdate.Email = user.Email;
            userToUpdate.NormalizedEmail = user.Email.ToUpper();
            userToUpdate.PhoneNumber = user.PhoneNumber;

            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();

            return userToUpdate;
        }

        private string AuthenticatedUserRole()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

                return result;
            }
            else if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == false)
            {
                return "unauthorized";
            }
            else
            {
                throw new UnauthorizedAccessException("Can't get credentials of unauthorized user");
            }
        }
        //private string AuthenticatedUserId()
        //{
        //    if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        return result;
        //    }
        //    else
        //    {
        //        throw new UnauthorizedAccessException("Can't get credentials of unauthorized user");
        //    }
        //}

        

        //public void SeedUserRoles(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        //        new IdentityUserRole<string> { RoleId = "33B7ED72-9434-434A-82D4-3018B018CB87", UserId = "8716071C-1D9B-48FD-B3D0-F059C4FB8031" },
        //        new IdentityUserRole<string> { RoleId = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", UserId = "D514EDC9-94BB-416F-AF9D-7C13669689C9" },
        //        new IdentityUserRole<string> { RoleId = "9C07F9F6-D3B0-458A-AB7F-218AA622FA5B", UserId = "87746F88-DC38-4756-924A-B95CFF3A1D8A" }
        //        );
        //}
    }
}
