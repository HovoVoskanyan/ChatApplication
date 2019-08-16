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
    public class ChatManager
    {
        public ChatRepository _chatRepository = new ChatRepository();
        public ChatManager()
        {

        }

        public ResponseResult<bool> SendMessage(ChatModel chatModel)
        {

            try 
	        {
                _chatRepository.SendMessage(new ChatDTO()
                {
                    Text = chatModel.Text,
                    UserIdTo = chatModel.UserIdTo,
                    UserIdFrom = SessionInfo.CurrentUserInfo.Id
                });
                return new ResponseResult<bool>()
                {
                    Message = "Success",
                    Type = Enums.ResponseType.Success
                };
            }
	    catch (Exception e)
	    {

                return new ResponseResult<bool>()
                {
                    Message=e.Message,
                    Type=Enums.ResponseType.Error
                };
	    }

        }
        public List<ChatModel> GetMessages(int selectedUserId)
        {
           return _chatRepository.GetMessages(selectedUserId,SessionInfo.CurrentUserInfo.Id).Select(s => new ChatModel()
            {
                Text = s.Text,
                UserIdTo =s.UserIdTo
            }).ToList();
            
                
        }
    }
}
