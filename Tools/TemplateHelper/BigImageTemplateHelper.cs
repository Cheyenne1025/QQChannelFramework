using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MyBot.Tools.TemplateHelper;

sealed partial class MessageTemplateHelper
{
    BigImageTemplateHelper _bigImageTemplateHelper;

    /// <summary>
    /// 获取大图消息模版构建器实例
    /// </summary>
    /// <returns></returns>
    public BigImageTemplateHelper GetBigImageTemplateHelper()
    {
        _bigImageTemplateHelper = new();

        return _bigImageTemplateHelper;
    }
}

/// <summary>
/// 大图消息模版构建类
/// </summary>
public class BigImageTemplateHelper
{
    const string PromptType = "#PROMPT#";
    const string TitleType = "#METATITLE#";
    const string SubTitleType = "#METASUBTITLE#";
    const string ImageType = "#METACOVER#";
    const string JumpType = "#METAURL#";

    Dictionary<string, object> _dic;

    JObject _template;

    List<Dictionary<string, object>> _kvs;

    public BigImageTemplateHelper()
    {
        _dic = new Dictionary<string, object>()
            {
            {"msg_id", string.Empty},
                {"ark",new Dictionary<string,object>()
                {
                    {"template_id",37 },
                    {"kv",new List<Dictionary<string,object>>()
                    {
                        new()
                        {
                            {"key",PromptType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",TitleType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",SubTitleType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",ImageType },
                            {"value",string.Empty }
                        },
                        new()
                        {
                            {"key",JumpType },
                            {"value",string.Empty }
                        }
                    }
                    }
                }
                }
            };

        _template = JObject.Parse(JsonConvert.SerializeObject(_dic));
    }

    /// <summary>
    /// 设置提示消息
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public BigImageTemplateHelper SetPrompt(string prompt)
    {
        _template["ark"]["kv"][0]["value"] = prompt;

        return this;
    }

    /// <summary>
    /// 携带回复的消息ID
    /// </summary>
    /// <param name="messageId"></param>
    /// <returns></returns>
    public BigImageTemplateHelper WithReplyMessageId(string messageId)
    {
        _template["msg_id"] = messageId;

        return this;
    }

    /// <summary>
    /// 设置主标题
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public BigImageTemplateHelper SetTitle(string title)
    {
        _template["ark"]["kv"][1]["value"] = title;

        return this;
    }

    /// <summary>
    /// 设置子标题
    /// </summary>
    /// <param name="subTitle"></param>
    /// <returns></returns>
    public BigImageTemplateHelper SetSubTitle(string subTitle)
    {
        _template["ark"]["kv"][2]["value"] = subTitle;

        return this;
    }

    /// <summary>
    /// 设置图片
    /// </summary>
    /// <param name="url">图片链接</param>
    /// <returns></returns>
    public BigImageTemplateHelper SetImage(string url)
    {
        _template["ark"]["kv"][3]["value"] = url;

        return this;
    }

    /// <summary>
    /// 设置跳转链接
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public BigImageTemplateHelper SetLink(string link)
    {
        _template["ark"]["kv"][4]["value"] = link;

        return this;
    }

    /// <summary>
    /// 构建模版
    /// </summary>
    /// <returns></returns>
    public JObject BuildTemplate()
    {
        return _template;
    }
}