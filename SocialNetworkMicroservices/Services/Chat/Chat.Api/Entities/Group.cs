using System.ComponentModel.DataAnnotations;

namespace Chat.Api.Entities
{
    public class Group
    {
        public Group(string name)
        {
            Name = name;
        }

        [Key]
        public string Name { get; set; }
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }

    public class Connection
    {
        public Connection(string connectionId, string userName)
        {
            ConnectionId = connectionId;
            UserName = userName;
        }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
    }
}
