using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moodle
{
    public class MoodleMessage
    {
        int intToUserId;
        string strText;
        int intTextFormat;
        int intClientMsgId;

        public int ToUserId
        {
            get { return intToUserId; }
            set { intToUserId = value; }
        }

        public string Text
        {
            get { return strText; }
            set { strText = value; }
        }

        public int TextFormat
        {
            get { return intTextFormat; }
            set { intTextFormat = value; }
        }

        public int ClientMsgId
        {
            get { return intClientMsgId; }
            set { intClientMsgId = value; }
        }

        public MoodleMessage() { }

        public MoodleMessage(int touserid, string text, int textformat, int clientmsgid)
        {
            intToUserId = touserid;
            strText = text;
            intTextFormat = textformat;
            intClientMsgId = clientmsgid;
        }

        public static string SendInstantMessages(List<MoodleMessage> list, string token)
        {
            string postData = "?wstoken=" + token + "&wsfunction=core_message_send_instant_messages";

            for (int i = 0; i < list.Count; i++)
            {
                postData += "&messages[" + i + "][touserid]=" + list[i].ToUserId;
                postData += "&messages[" + i + "][text]=" + HttpUtility.UrlEncode(list[i].Text);
                postData += "&messages[" + i + "][textformat]=" + list[i].TextFormat;
                postData += "&messages[" + i + "][clientmsgid]=" + list[i].ClientMsgId;
            }

            MoodleWebRequest web = new MoodleWebRequest(MoodleUrl.RestUrl + postData);
            return web.GetResponse();
        }
    }
}