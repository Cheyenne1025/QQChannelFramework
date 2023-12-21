using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBot.Models;
namespace MyBot.Expansions.Bot;

sealed partial class ChannelBot
{
    private static Dictionary<string, Action<CommandInfo>?> _commands = new();

    private static Dictionary<string, (List<Action<CommandInfo, CommandState>>,Dictionary<string, CommandState>)> _stepCommands = new();

    private static List<string> _ChangeingCommands = new();

    /// <summary>
    /// 注册普通指令
    /// </summary>
    /// <param name="command">文字指令 (字符敏感)</param>
    /// <param name="commandAction">指令执行</param>
    /// <returns></returns>
    public ChannelBot RegisterCommand(string command, Action<CommandInfo> commandAction)
    {
        if(_commands.ContainsKey(command) is true)
        {
            throw new Exception($"指令 {command} 已存在");
        }

        _commands.Add(command, commandAction);

        return this;
    }

    /// <summary>
    /// 卸载指令
    /// </summary>
    /// <param name="command">指令</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public ChannelBot UnloadCommand(string command)
    {
        bool result1, result2;

        result1 = _commands.ContainsKey(command);
        result2 = _stepCommands.ContainsKey(command);

        if (result1 is false && result2 is false)
        {
            throw new Exception($"指令 {command} 不存在");
        }

        if(result1)
        {
            _commands.Remove(command);
        }

        if(result2)
        {
            _stepCommands.Remove(command);
        }

        return this;
    }

    /// <summary>
    /// 更换普通指令的处理函数 (替换后3秒后生效)
    /// </summary>
    /// <param name="command">指令</param>
    /// <param name="newCommandAction">新处理函数</param>
    /// <returns></returns>
    public async Task<ChannelBot> ChangeActionAsync(string command, Action<CommandInfo> newCommandAction)
    {
        if(_ChangeingCommands.Contains(command) is false)
        {
            _ChangeingCommands.Add(command);

            await Task.Delay(3000);

            if (_commands.ContainsKey(command) is false)
            {
                throw new Exception($"指令 {command} 不存在");
            }

            _commands[command] = newCommandAction;

            _ChangeingCommands.Remove(command);
        }

        return this;
    }

    /// <summary>
    /// 注册步骤指令
    /// </summary>
    /// <param name="command"></param>
    /// <param name="commandActions"></param>
    /// <returns></returns>
    public ChannelBot RegisterStepCommand(string command,params Action<CommandInfo,CommandState>[] commandActions)
    {
        if(_commands.ContainsKey(command) || _stepCommands.ContainsKey(command))
        {
            throw new Exception("指令只能同时注册为一种类型，要么是普通指令，要么是步骤指令");
        }

        _stepCommands.Add(command, (commandActions.ToList(), new Dictionary<string, CommandState>()));

        return this;
    }

    /// <summary>
    /// 删除步骤指令状态
    /// </summary>
    /// <param name="command"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public ChannelBot RemoveCommandState(string command,string userId)
    {
        if(_stepCommands.ContainsKey(command))
        {
            var (_, states) = _stepCommands[command];

            if(states.ContainsKey(userId))
            {
                states.Remove(userId);
            }
        }

        return this;
    }

    /// <summary>
    /// 更换步骤指令的处理函数 (替换后3秒生效)
    /// </summary>
    /// <param name="command">指令</param>
    /// <param name="step">替换第几步</param>
    /// <param name="newCommandAction">新处理函数</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ChannelBot> ChangeStepCommandActionAsync(string command, int step, Action<CommandInfo, CommandState> newCommandAction)
    {
        if (_stepCommands.ContainsKey(command) is false)
        {
            throw new Exception($"指令 {command} 不存在，或不为步骤指令");
        }

        var (actions, _) = _stepCommands[command];

        if (step - 1 >= actions.Count)
        {
            throw new Exception($"步骤指令 {command} 中，处理函数不存在 第 {step} 步骤");
        }

        await Task.Delay(3000);

        actions[step - 1] = newCommandAction;

        return this;
    }

    private void InvokeCommand(CommandInfo commandInfo,out bool trigger)
    {
        if (commandInfo.Key is null)
        {
            trigger = false;
            return;
        }

        if (_commands.ContainsKey(commandInfo.Key) is true)
        {
            trigger = true;
            _commands[commandInfo.Key](commandInfo);
        }
        else if(_stepCommands.ContainsKey(commandInfo.Key) is true)
        {
            trigger = true;

            var (actions, states) = _stepCommands[commandInfo.Key];

            if(states.ContainsKey(commandInfo.Sender.Id))
            {
                if(states[commandInfo.Sender.Id].Step >= actions.Count)
                {
                    states[commandInfo.Sender.Id].Reset();
                }

                actions[states[commandInfo.Sender.Id].Step](commandInfo, states[commandInfo.Sender.Id]);
            }
            else
            {
                CommandState state = new()
                {
                    MaxStep = actions.Count,
                    Step = 0
                };

                states.Add(commandInfo.Sender.Id, state);

                actions[0](commandInfo, state);
            }
        }
        else
        {
            trigger = false;
        }
    }
}