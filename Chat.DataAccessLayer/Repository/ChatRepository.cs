using Chat.DataAccessLayer.DTO;
using Chat.DataAccessLayer.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccessLayer.Repository
{
    public class ChatRepository
    {
        private readonly MicrosoftChatEntities _context;
        public ChatRepository()
        {
            _context = new MicrosoftChatEntities();
        }


        public void SendMessage(ChatDTO chatDTO)
        {
            _context.Messages.Add(new Message()
            {
                Text = chatDTO.Text,
                UserIdFrom = chatDTO.UserIdFrom,
                UserIdTo = chatDTO.UserIdTo,
                CreatedOn = DateTime.Now
            });
            _context.SaveChanges();
        }
        public List<ChatDTO> GetMessages(int selectedUserId,int currentId)
        {
            var query = (from message in _context.Messages
                         where (message.UserIdFrom == currentId && message.UserIdTo == selectedUserId) ||
                         (message.UserIdFrom == selectedUserId && message.UserIdTo == currentId)
                         select new
                         {
                             message.Text,
                             message.UserIdTo,
                             message.CreatedOn

                         }).OrderBy(o => o.CreatedOn).Select(s => new ChatDTO()
                         {
                             UserIdTo = s.UserIdTo,
                             Text = s.Text
                             
                         });
            var listOfMessages = query.ToList();
            return listOfMessages;
        }
    }
}
