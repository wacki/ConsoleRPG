using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG {
    // Custom message manager class.
    // User can dynamically add new messages to a dictionary with a unique name
    // And can later decide to output a random message from one of these keys.
    class MessageManager {
        static MessageManager m_soInstance = null;
        public static MessageManager instance { get { return m_soInstance; } }
        
        // Dictionary containing our messages
        private Dictionary<string, List<string>> _messages = new Dictionary<string, List<string>>();

        public static void Init()
        {
            if(instance != null) {
                throw new Exception("ERROR: trying to instantiate multiple message manager instances");
            }

            m_soInstance = new MessageManager();
        }

        // private constructor, preventing instantiation outside of this class
        private MessageManager()
        { }

        // Function actually adding a new message to the dictionary
        public void AddMsg(string key, string msg)
        {
            // List variable to store the reference to the list we want to add our message to
            List<string> messageList;

            // Try and add the message to an existing key
            if(_messages.TryGetValue(key, out messageList)) {
                messageList.Add(msg);
            }
            // If the key doesn't exist, create a new list and add the message to that
            else {
                messageList = new List<string>();
                messageList.Add(msg);
                _messages.Add(key, messageList);
            }
        }


        // Function printing a random message from a selected key
        // optionally the user can supply arguments the message can use
        // similar to Console.WriteLine
        public void PrintRandomMsg(string key, params object[] args)
        {
            Console.WriteLine(GetRandomMsg(key, args));
        }

        // Function returning a random message from the dictionary based on the key
        public string GetRandomMsg(string key, params object[] args)
        {
            Random rand = new Random();
            List<string> messageList;
            // Try and get our list from the dictionary
            if(_messages.TryGetValue(key, out messageList)) {
                return string.Format(messageList[rand.Next(0, messageList.Count)], args);
            }

            return "MISSING MESSAGE!";
        }
    }
}
