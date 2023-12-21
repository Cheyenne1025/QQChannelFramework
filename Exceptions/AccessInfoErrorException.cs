namespace MyBot.Exceptions
{
    public class AccessInfoErrorException : Exception
    {
        public AccessInfoErrorException() : base("鉴权信息有误，请检查以下可能的原因: \n" +
            "1. 鉴权信息是否正确\n" +
            "2. 主频道Guild是否正确\n" +
            "3. 子频道Id是否正确\n" +
            "4. 机器人的相应权限是否开启\n" +
            "5. 将机器人移除频道再添加进来")
        {
        }
    }
}