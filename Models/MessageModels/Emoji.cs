using Newtonsoft.Json;

namespace MyBot.Models.MessageModels;

/// <summary>
/// 表情
/// </summary>
public class Emoji {
   /// <summary>
   /// 表情ID
   /// </summary>
   [JsonProperty("id")]
   public string Id { get; set; }

   /// <summary>
   /// 表情类型
   /// </summary>
   [JsonProperty("type")]
   public int Type { get; set; }

   public Emoji() { }

   /// <summary>
   /// 必须是Emoji字符
   /// </summary>
   /// <param name="emoji"></param>
   public Emoji(char emoji) {
      Type = 2;
      Id = ((int) emoji).ToString();
   }

   public static bool operator ==(Emoji lhs, Emoji rhs) {
      if (ReferenceEquals(lhs, rhs))
         return true;
      if (ReferenceEquals(lhs, null))
         return false;
      if (ReferenceEquals(rhs, null))
         return false;
      return lhs.Id == rhs.Id && lhs.Type == rhs.Type;
   }

   public static bool operator !=(Emoji lhs, Emoji rhs) {
      return !(lhs == rhs);
   }
   
   public static bool operator ==(Emoji lhs, char rhs) { 
      if (ReferenceEquals(lhs, null))
         return false;
      return lhs.Type == 2 && lhs.Id == ((int) rhs).ToString();
   }
   
   public static bool operator !=(Emoji lhs, char rhs) {
      return !(lhs == rhs);
   }
}