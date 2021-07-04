using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using uk.JohnCook.dotnet.EditableCMDLibrary.Commands;
using uk.JohnCook.dotnet.EditableCMDLibrary.ConsoleSessions;
using uk.JohnCook.dotnet.EditableCMDLibrary.InputProcessing;
using uk.JohnCook.dotnet.EditableCMDLibrary.Interop;
using uk.JohnCook.dotnet.EditableCMDLibrary.Logging;
using uk.JohnCook.dotnet.EditableCMDLibrary.Utils;

/// <summary>
/// EditableCMD Plugin $safeprojectname$
/// </summary>
namespace $safeprojectname$
{
    /// <summary>
    /// Class for handling Command Template
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class Template : ICommandInput
    {
        #region Plugin Implementation Details
        /// <inheritdoc cref="ICommandInput.Name"/>
        public string Name => "Template";
        /// <inheritdoc cref="ICommandInput.Description"/>
        /// TODO: Change plugin description
        public string Description => "Handles command TEMPLATE, doing nothing.";
        /// <inheritdoc cref="ICommandInput.AuthorName"/>
        /// TODO: Change plugin creator's name
        public string AuthorName => "$username$";
        /// <inheritdoc cref="ICommandInput.AuthorTwitchUsername"/>
        /// TODO: Set plugin creator's Twitch username
        public string AuthorTwitchUsername => string.Empty;
        /// <inheritdoc cref="ICommandInput.KeysHandled"/>
        public ConsoleKey[]? KeysHandled => new ConsoleKey[] { ConsoleKey.Enter };
        /// <inheritdoc cref="ICommandInput.NormalModeHandled"/>
        public bool NormalModeHandled => true;
        /// <inheritdoc cref="ICommandInput.EditModeHandled"/>
        public bool EditModeHandled => false;
        /// <inheritdoc cref="ICommandInput.MarkModeHandled"/>
        public bool MarkModeHandled => false;
        /// <inheritdoc cref="ICommandInput.CommandsHandled"/>
        /// TODO: Change command(s) the plugin handles
        public string[]? CommandsHandled => new string[] { "template" };
        #endregion

        private string regexCommandString = string.Empty;
        private ConsoleState? state;

        /// <inheritdoc cref="ICommandInput.Init(ConsoleState)"/>
        [MemberNotNull(nameof(state))]
        public void Init(ConsoleState state)
        {
            // Add all commands listed in CommandsHandled to the regex string for matching if this plugin handles the command.
            if (KeysHandled?.Contains(ConsoleKey.Enter) == true && CommandsHandled?.Length > 0)
            {
                regexCommandString = string.Concat("^(", string.Join('|', CommandsHandled), ")( .*)?$");
            }
            this.state = state;
        }

        /// TODO: Change XMLDOC summary
        /// <summary>
        /// Event handler for the TEMPLATE command.
        /// </summary>
        /// <inheritdoc cref="ICommandInput.ProcessCommand(object?, NativeMethods.ConsoleKeyEventArgs)" path="param"/>
        public void ProcessCommand(object? sender, NativeMethods.ConsoleKeyEventArgs e)
        {
            // Call Init() again if state isn't set
            if (state == null)
            {
                Init(e.State);
            }
            // Return early if we're not interested in the event
            if (e.Handled || // Event has already been handled
                !e.Key.KeyDown || // A key was not pressed
                KeysHandled?.Contains(e.Key.ConsoleKey) != true || // The key pressed wasn't one we handle
                state.EditMode // Edit mode is enabled
                )
            {
                return;
            }
            // We're handling the event if these conditions are met
            else if (!string.IsNullOrEmpty(regexCommandString) && Regex.Match(state.Input.Text.ToString().Trim(), regexCommandString, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Success)
            {
                e.Handled = true;
            }
            // In all other cases we are not handling the event
            else
            {
                return;
            }

            // TODO: Change plugin functionality

            // Do nothing
            return;
        }
    }
}
