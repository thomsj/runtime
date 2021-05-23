// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Reflection;

namespace System.IO.Tests
{
    /// <summary>
    /// Helper to expose protected and private members for testing
    /// </summary>
    public class TestFileSystemWatcher : FileSystemWatcher
    {
        private delegate void NotifyFileSystemEventArgs(WatcherChangeTypes changeType, ReadOnlySpan<char> name);
        private delegate void NotifyRenameEventArgs(WatcherChangeTypes action, ReadOnlySpan<char> name, ReadOnlySpan<char> oldName);

        public TestFileSystemWatcher() : base()
        {
        }

        public TestFileSystemWatcher(string path, string filter) : base(path, filter)
        {
        }

        public void CallOnChanged(FileSystemEventArgs e)
        {
            this.OnChanged(e);
        }

        public void CallOnCreated(FileSystemEventArgs e)
        {
            this.OnCreated(e);
        }

        public void CallOnDeleted(FileSystemEventArgs e)
        {
            this.OnDeleted(e);
        }

        public void CallOnError(ErrorEventArgs e)
        {
            this.OnError(e);
        }

        public void CallOnRenamed(RenamedEventArgs e)
        {
            this.OnRenamed(e);
        }

        public void CallNotifyFileSystemEventArgs(WatcherChangeTypes changeType, ReadOnlySpan<char> name)
        {
            MethodInfo notifyFileSystemEventArgs = typeof(FileSystemWatcher).GetMethod(
                "NotifyFileSystemEventArgs",
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new Type[] { typeof(WatcherChangeTypes), typeof(ReadOnlySpan<char>) },
                null);

            notifyFileSystemEventArgs.CreateDelegate<NotifyFileSystemEventArgs>(this)(changeType, name);
        }

        public void CallNotifyInternalBufferOverflowEvent()
        {
            MethodInfo notifyInternalBufferOverflowEvent = typeof(FileSystemWatcher).GetMethod(
                "NotifyInternalBufferOverflowEvent",
                BindingFlags.Instance | BindingFlags.NonPublic);

            notifyInternalBufferOverflowEvent.Invoke(this, null);
        }

        public void CallNotifyRenameEventArgs(WatcherChangeTypes action, ReadOnlySpan<char> name, ReadOnlySpan<char> oldName)
        {
            MethodInfo notifyRenameEventArgs = typeof(FileSystemWatcher).GetMethod(
                "NotifyRenameEventArgs",
                BindingFlags.Instance | BindingFlags.NonPublic);

            notifyRenameEventArgs.CreateDelegate<NotifyRenameEventArgs>(this)(action, name, oldName);
        }
    }
}
