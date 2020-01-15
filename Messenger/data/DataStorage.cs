using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger
{
    class DataStorage: IRepository
    {
        public static DataStorage Instance { get; } = new DataStorage();

        private DataStorage()
        {
            usersWatcher = new FileSystemWatcher();
            usersWatcher.Path = AppDomain.CurrentDomain.BaseDirectory;
            messagesWatcher = new FileSystemWatcher();
            messagesWatcher.Path = AppDomain.CurrentDomain.BaseDirectory;

            usersWatcher.NotifyFilter = NotifyFilters.LastWrite;
            messagesWatcher.NotifyFilter = NotifyFilters.LastWrite;

            usersWatcher.Filter = usersFile;
            usersWatcher.EnableRaisingEvents = true;
            messagesWatcher.Filter = messagesFile;
            messagesWatcher.EnableRaisingEvents = true;
        }
        
        private const string usersFile = "users.txt";
        private const string messagesFile = "messages.txt";
        private const int maxAttemptCount = 42;
        private const int attemptInterval = 250;
        private FileSystemWatcher usersWatcher;
        private FileSystemWatcher messagesWatcher;
        private Logger logger = Logger.Instance;

        public void updateMessagesList(List<Message> messages)
        {
            DataSet ds = new DataSet("Root");
            DataTable dt = getMessageTable();
            ds.Tables.Add(dt);

            messages.ForEach(message => {
                DataRow dr = dt.Rows.Add();
                dr[0] = message.ID;
                dr[1] = message.SenderID;
                dr[2] = message.ReceiverID;
                dr[3] = message.Date;
                dr[4] = message.isRead;
                dr[5] = message.Content;
            });

            writeData(messagesFile, ds);
        }

        public void updateUsersList(List<User> users)
        {
            DataSet ds = new DataSet("Root");
            DataTable dt = getUserTable();
            ds.Tables.Add(dt);

            users.ForEach(user => {
                DataRow dr = dt.Rows.Add();
                dr[0] = user.ID;
                dr[1] = user.Name;
            });

            writeData(usersFile, ds);
        }

        public List<User> getAllUsers()
        {
            List<User> users = new List<User>();

            DataTable table = readData(usersFile, Tables.User.ToString());
            if(table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = int.Parse(row.ItemArray[0].ToString());
                    String name = row.ItemArray[1].ToString();
                    users.Add(new User(id) { Name = name });
                }
            }

            return users;
        }

        public List<Message> getAllMessages()
        {
            List<Message> res = new List<Message>();

            DataTable table = readData(messagesFile, Tables.Message.ToString());
            if(table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = int.Parse(row.ItemArray[0].ToString());
                    int senderID = int.Parse(row.ItemArray[1].ToString());
                    int receiverID = int.Parse(row.ItemArray[2].ToString());
                    long date = long.Parse(row.ItemArray[3].ToString());
                    bool isRead = bool.Parse(row.ItemArray[4].ToString());
                    String content = row.ItemArray[5].ToString();
                    res.Add(new Message(id)
                    {
                        SenderID = senderID,
                        ReceiverID = receiverID,
                        Date = date,
                        isRead = isRead,
                        Content = content
                    }
                    );
                }
            }

            return res;
        }

        public void registerUserListener(FileSystemEventHandler handler)
        {
            usersWatcher.Changed += handler;
        }

        public void registerMessagesWatcher(FileSystemEventHandler handler)
        {
            messagesWatcher.Changed += handler;
        }

        private DataTable readData(string fileName, string tableName)
        {
            DataTable res = new DataTable();

            int attemps = 0;
            while (attemps < maxAttemptCount)
            {
                try
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(fs, XmlReadMode.InferTypedSchema);
                        fs.Close();

                        res = ds.Tables[tableName];
                    }
                    break;
                }
                catch (Exception e)
                {
                    logger.log(e);
                    Thread.Sleep(attemptInterval);
                }
            }

            if (attemps == maxAttemptCount)
            {
                MessageBox.Show("Read data failed :(", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return res;
        }

        private void writeData(string path, DataSet dataSet)
        {
            int attemps = 0;
            while (attemps < maxAttemptCount)
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        dataSet.WriteXml(fs);
                    }
                    break;
                }
                catch (Exception e)
                {
                    logger.log(e);
                }
            }
            if (attemps == maxAttemptCount)
            {
                MessageBox.Show("Write data failed :(", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DataTable getUserTable()
        {
            DataTable dt = new DataTable(Tables.User.ToString());

            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));

            return dt;
        }

        private DataTable getMessageTable()
        {
            DataTable dt = new DataTable(Tables.Message.ToString());

            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("sender_id", typeof(int));
            dt.Columns.Add("receiver_id", typeof(int));
            dt.Columns.Add("date", typeof(long));
            dt.Columns.Add("is_read", typeof(bool));
            dt.Columns.Add("content", typeof(string));

            return dt;
        }

        private enum Tables
        {
            User, Message
        }
    }
}
