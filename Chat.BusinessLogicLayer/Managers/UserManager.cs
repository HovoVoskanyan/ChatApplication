using Chat.BusinessLogicLayer.Enums;
using Chat.BusinessLogicLayer.Models;
using Chat.DataAccessLayer.DTO;
using Chat.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.BusinessLogicLayer.Managers
{
    public class UserManager
    {
        private readonly UserRepository _userRepository;
       // private static List<UserModel> users = new List<UserModel>();
        public UserManager()
        {
            _userRepository = new UserRepository();   

        }

        public ResponseResult<bool> Login(UserModel user)
        {
            try
            {
                var currentUser = _userRepository.GetUser(new UserDTO()
                {
                    
                    UserName = user.UserName,
                    Password = user.Password
                });
                if (currentUser != null)
                {
                    SessionInfo.CurrentUserInfo = new UserModel()
                    {
                        Id=currentUser.Id,
                        UserName=currentUser.UserName,
                        Password=currentUser.Password
                    };
                    return new ResponseResult<bool>()
                    {
                        Data = true,
                        Type = ResponseType.Success,
                        Message = "User Sign In Succesfully"
                    };
                }
                return new ResponseResult<bool>()
                {
                    Type = ResponseType.Error,
                    Message = "Username or password  incorrect"
                };
            }
            catch (Exception e)
            {
                return new ResponseResult<bool>()
                {
                    Type = ResponseType.Error,
                    Message = e.Message
                };
            }
        }

        public ResponseResult<bool> Registrate(UserRegistrationModel user)
        {

            try
            {

                if (!string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.UserName))
                {
                    if (user.Password.Equals(user.ConfirmPassword))
                    {
                        if (!_userRepository.IsUserExist(user.UserName))
                        {
                            _userRepository.Add(new UserDTO()
                            {
                                UserName=user.UserName,
                                Password=user.Password
                            });
                            return new ResponseResult<bool>()
                            {
                                Type = ResponseType.Success,
                                Message = "Congratulations You Have Registered"
                            };
                        }
                        else
                        {
                            return new ResponseResult<bool>()
                            {
                                Type = ResponseType.Information,
                                Message = "This Usernam already exists"
                            };
                        }
                    }

                    else
                    {
                        return new ResponseResult<bool>()
                        {
                            Type = ResponseType.Information,
                            Message = "Your Passwords don't match"
                        };
                    }
                }
                else
                {
                    return new ResponseResult<bool>()
                    {
                        Type = ResponseType.Error,
                        Message = "Please Fill your Login or Password"
                    };

                }

            }
            catch (Exception e)
            {
                return new ResponseResult<bool>()
                {
                    Type = ResponseType.Error,
                    Message = e.Message
                };
            }
        }

        public List<UserModel> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers(SessionInfo.CurrentUserInfo.Id);


            return users.Select(s => new UserModel()
            {
                Id = s.Id,
                UserName = s.UserName
            }).ToList();
        }
    }
}

        
        
    

