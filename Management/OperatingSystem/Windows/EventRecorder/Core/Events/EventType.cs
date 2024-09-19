using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinityOfficialNetwork.Management.OperatingSystem.Windows.EventRecorder.Core.Events
{
    public class EventType
    {
        [Key] public ulong Id { get; init;  }

        public EventType() { }
        public EventType(Raw.EventType eventType, string rawEvent)
        {
            RawEvent = rawEvent;

            if (eventType.System != null)
                System = new SystemPropertiesType(eventType.System);
            if (eventType.Item as byte[] != null)
            {
                List<byte> bytes = new List<byte>(eventType.Item as byte[]);
                BinaryEventData = bytes.ToArray();
            }
            if (eventType.Item as Raw.DebugDataType != null)
                DebugData = new DebugDataType(eventType.Item as Raw.DebugDataType);
            if (eventType.Item as Raw.EventDataType != null)
                EventData = new EventDataType(eventType.Item as Raw.EventDataType);
            if (eventType.Item as Raw.ProcessingErrorDataType != null)
                ProcessingErrorData = new ProcessingErrorDataType(eventType.Item as Raw.ProcessingErrorDataType);
            if (eventType.Item as Raw.UserDataType != null)
                UserData = new UserDataType(eventType.Item as Raw.UserDataType);
            if (eventType.RenderingInfo != null)
                RenderingInfo = new RenderingInfoType(eventType.RenderingInfo);
            RawEvent = rawEvent;
        }

        public string RawEvent { get; init; }

        public SystemPropertiesType? System { get; init; }
        public byte[]? BinaryEventData { get; init; }
        public DebugDataType? DebugData { get; init; }
        public EventDataType? EventData { get; init; }
        public ProcessingErrorDataType? ProcessingErrorData { get; init; }
        public UserDataType? UserData { get; init; }
        public RenderingInfoType? RenderingInfo { get; init; }

        public class SystemPropertiesType
        {
            [Key] public ulong Id { get; init; }
            public SystemPropertiesType() { }

            public SystemPropertiesType(Raw.SystemPropertiesType systemPropertiesType)
            {
                if (systemPropertiesType.Provider != null)
                    Provider = new ProviderType(systemPropertiesType.Provider);
                if (systemPropertiesType.EventID != null)
                    EventID = new EventIDType(systemPropertiesType.EventID);
                Version = (systemPropertiesType.Version);
                VersionSpecified = (systemPropertiesType.VersionSpecified);
                Level = (systemPropertiesType.Level);
                LevelSpecified = (systemPropertiesType.LevelSpecified);
                Task = (systemPropertiesType.Task);
                TaskSpecified = (systemPropertiesType.TaskSpecified);
                Opcode = (systemPropertiesType.Opcode);
                OpcodeSpecified = (systemPropertiesType.OpcodeSpecified);
                if (systemPropertiesType.Keywords != null)
                    Keywords = new string(systemPropertiesType.Keywords);
                if (systemPropertiesType.TimeCreated != null)
                    TimeCreated = new TimeCreatedType(systemPropertiesType.TimeCreated);
                if (systemPropertiesType.EventRecordID != null)
                    EventRecordID = new EventRecordIDType(systemPropertiesType.EventRecordID);
                if (systemPropertiesType.Correlation != null)
                    Correlation = new CorrelationType(systemPropertiesType.Correlation);
                if (systemPropertiesType.Execution != null)
                    Execution = new ExecutionType(systemPropertiesType.Execution);
                if (systemPropertiesType.Channel != null)
                    Channel = new string(systemPropertiesType.Channel);
                if (systemPropertiesType.Computer != null)
                    Computer = new string(systemPropertiesType.Computer);
                if (systemPropertiesType.Container != null)
                    Container = new string(systemPropertiesType.Container);
                if (systemPropertiesType.Security != null)
                    Security = new SecurityType(systemPropertiesType.Security);
            }

            public ProviderType? Provider { get; init; }
            public EventIDType? EventID { get; init; }
            public sbyte? Version { get; init; }
            public bool? VersionSpecified { get; init; }
            public sbyte? Level { get; init; }
            public bool? LevelSpecified { get; init; }
            public ushort? Task { get; init; }
            public bool? TaskSpecified { get; init; }
            public sbyte? Opcode { get; init; }
            public bool? OpcodeSpecified { get; init; }
            public string? Keywords { get; init; }
            public TimeCreatedType? TimeCreated { get; init; }
            public EventRecordIDType? EventRecordID { get; init; }
            public CorrelationType? Correlation { get; init; }
            public ExecutionType? Execution { get; init; }
            public string? Channel { get; init; }
            public string? Computer { get; init; }
            public string? Container { get; init; }
            public SecurityType? Security { get; init; }

            public class ProviderType
            {
                [Key] public ulong Id { get; init; }
                public ProviderType() { }
                public ProviderType(Raw.SystemPropertiesTypeProvider systemPropertiesTypeProvider)
                {
                    if (systemPropertiesTypeProvider.Name != null)
                        Name = new string(systemPropertiesTypeProvider.Name);
                    if (systemPropertiesTypeProvider.Guid != null)
                        Guid = new string(systemPropertiesTypeProvider.Guid);
                    if (systemPropertiesTypeProvider.EventSourceName != null)
                        EventSourceName = new string(systemPropertiesTypeProvider.EventSourceName);
                }

                public string? Name { get; init; }
                public string? Guid { get; init; }
                public string? EventSourceName { get; init; }
            }

            public class EventIDType
            {
                [Key] public ulong Id { get; init; }
                public EventIDType() { }
                public EventIDType(Raw.SystemPropertiesTypeEventID systemPropertiesTypeEventID)
                {
                    Qualifiers = systemPropertiesTypeEventID.Qualifiers;
                    Value = systemPropertiesTypeEventID.Value;
                }

                public ushort? Qualifiers { get; init; }
                public ushort? Value { get; init; }
            }

            public class TimeCreatedType
            {
                [Key] public ulong Id { get; init; }
                public TimeCreatedType() { }
                public TimeCreatedType(Raw.SystemPropertiesTypeTimeCreated systemPropertiesTypeTimeCreated)
                {
                    SystemTime = systemPropertiesTypeTimeCreated.SystemTime;
                }

                public System.DateTime? SystemTime { get; init; }
            }

            public class EventRecordIDType
            {
                [Key] public ulong Id { get; init; }
                public EventRecordIDType() { }
                public EventRecordIDType(Raw.SystemPropertiesTypeEventRecordID systemPropertiesTypeEventRecordID)
                {
                    if (systemPropertiesTypeEventRecordID != null)
                        Value = systemPropertiesTypeEventRecordID.Value;
                }

                public ulong? Value { get; init; }
            }

            public class CorrelationType
            {
                [Key] public ulong Id { get; init; }
                public CorrelationType() { }
                public CorrelationType(Raw.SystemPropertiesTypeCorrelation systemPropertiesTypeCorrelation)
                {
                    if (systemPropertiesTypeCorrelation.ActivityID != null)
                        ActivityID = new string(systemPropertiesTypeCorrelation.ActivityID);
                    if (systemPropertiesTypeCorrelation.RelatedActivityID != null)
                        RelatedActivityID = new string(systemPropertiesTypeCorrelation.RelatedActivityID);
                }

                public string? ActivityID { get; init; }
                public string? RelatedActivityID { get; init; }
            }

            public class ExecutionType
            {
                [Key] public ulong Id { get; init; }
                public ExecutionType() { }
                public ExecutionType(Raw.SystemPropertiesTypeExecution systemPropertiesTypeExecution)
                {
                    ProcessID = systemPropertiesTypeExecution.ProcessID;
                    ThreadID = systemPropertiesTypeExecution.ThreadID;
                    ProcessorID = systemPropertiesTypeExecution.ProcessorID;
                    ProcessorIDSpecified = systemPropertiesTypeExecution.ProcessorIDSpecified;
                    SessionID = systemPropertiesTypeExecution.SessionID;
                    SessionIDSpecified = systemPropertiesTypeExecution.SessionIDSpecified;
                    KernelTime = systemPropertiesTypeExecution.KernelTime;
                    KernelTimeSpecified = systemPropertiesTypeExecution.KernelTimeSpecified;
                    UserTime = systemPropertiesTypeExecution.UserTime;
                    UserTimeSpecified = systemPropertiesTypeExecution.UserTimeSpecified;
                    ProcessorTime = systemPropertiesTypeExecution.ProcessorTime;
                    ProcessorTimeSpecified = systemPropertiesTypeExecution.ProcessorTimeSpecified;

                }

                public uint? ProcessID { get; init; }
                public uint? ThreadID { get; init; }
                public sbyte? ProcessorID { get; init; }
                public bool? ProcessorIDSpecified { get; init; }
                public uint? SessionID { get; init; }
                public bool? SessionIDSpecified { get; init; }
                public uint? KernelTime { get; init; }
                public bool? KernelTimeSpecified { get; init; }
                public uint? UserTime { get; init; }
                public bool? UserTimeSpecified { get; init; }
                public uint? ProcessorTime { get; init; }
                public bool? ProcessorTimeSpecified { get; init; }
            }

            public class SecurityType
            {
                [Key] public ulong Id { get; init; }
                public SecurityType() { }
                public SecurityType(Raw.SystemPropertiesTypeSecurity systemPropertiesTypeSecurity)
                {
                    if (systemPropertiesTypeSecurity.UserID != null)
                        UserID = new string(systemPropertiesTypeSecurity.UserID);
                }

                public string? UserID { get; init; }
            }


        }

        public class DebugDataType
        {
            [Key] public ulong Id { get; init; }
            public DebugDataType() { }
            public DebugDataType(Raw.DebugDataType debugDataType)
            {
                if (SequenceNumber != null) SequenceNumber = debugDataType.SequenceNumber;
                if (SequenceNumberSpecifiedified != null) SequenceNumberSpecifiedified = debugDataType.SequenceNumberSpecified;
                if (FlagsName != null) FlagsName = new(debugDataType.FlagsName);
                if (LevelName != null) LevelName = new(debugDataType.LevelName);
                if (Component != null) Component = new(debugDataType.Component);
                if (SubComponent != null) SubComponent = new(debugDataType.SubComponent);
                if (FileLine != null) FileLine = new(debugDataType.FileLine);
                if (Function != null) Function = new(debugDataType.Function);
                if (Message != null) Message = new(debugDataType.Message);

            }

            public uint? SequenceNumber { get; init; }
            public bool? SequenceNumberSpecifiedified { get; init; }
            public string? FlagsName { get; init; }
            public string? LevelName { get; init; }
            public string? Component { get; init; }
            public string? SubComponent { get; init; }
            public string? FileLine { get; init; }
            public string? Function { get; init; }
            public string? Message { get; init; }
        }

        public class EventDataType
        {
            [Key] public ulong Id { get; init; }
            public EventDataType() { }
            public EventDataType(Raw.EventDataType eventDataType)
            {
                if (eventDataType.Items != null)
                {
                    ComplexDataType = new List<ComplexDataType>();
                    DataType = new List<DataType>();
                    foreach (var item in eventDataType.Items)
                    {
                        if (item as Raw.DataType != null)
                            DataType.Add(new DataType(item as Raw.DataType));

                        if (item as Raw.ComplexDataType != null)
                            ComplexDataType.Add(new ComplexDataType(item as Raw.ComplexDataType));

                    }
                    if (ComplexDataType.Count == 0)
                        ComplexDataType = null;
                    if (DataType.Count == 0)
                        DataType = null;
                }
                if (eventDataType.Binary != null)
                {
                    List<byte> bytes = new List<byte>(eventDataType.Binary);
                    Binary = bytes.ToArray();
                }
                if (eventDataType.Name != null)
                    Name = eventDataType.Name;

            }

            public List<ComplexDataType>? ComplexDataType { get; init; }
            public List<DataType>? DataType { get; init; }
            public byte[]? Binary { get; init; }
            public string? Name { get; init; }
        }

        public class ProcessingErrorDataType
        {
            [Key] public ulong Id { get; init; }
            public ProcessingErrorDataType() { }
            public ProcessingErrorDataType(Raw.ProcessingErrorDataType processingErrorDataType)
            {
                ErrorCode = processingErrorDataType.ErrorCode;
                if (processingErrorDataType.DataItemName != null)
                    DataItemName = new string(processingErrorDataType.DataItemName);
                if (processingErrorDataType.EventPayload != null)
                {
                    List<byte> bytes = new List<byte>(processingErrorDataType.EventPayload);
                    EventPayload = bytes.ToArray();
                }
            }

            public uint? ErrorCode { get; init; }
            public string? DataItemName { get; init; }
            public byte[]? EventPayload { get; init; }
        }

        public class UserDataType
        {
            [Key] public ulong Id { get; init; }
            public UserDataType() { }
            public UserDataType(Raw.UserDataType userDataType)
            {
                //do nothing, have no clue how to convert
            }
        }

        public class RenderingInfoType
        {
            [Key] public ulong Id { get; init; }
            public RenderingInfoType() { }
            public RenderingInfoType(Raw.RenderingInfoType renderingInfoType)
            {
                if (renderingInfoType.Message != null)
                    Message = new string(renderingInfoType.Message);
                if (renderingInfoType.Level != null)
                    Level = new string(renderingInfoType.Level);
                if (renderingInfoType.Opcode != null)
                    Opcode = new string(renderingInfoType.Opcode);
                if (renderingInfoType.Task != null)
                    Task = new string(renderingInfoType.Task);
                if (renderingInfoType.Channel != null)
                    Channel = new string(renderingInfoType.Channel);
                if (renderingInfoType.Publisher != null)
                    Publisher = new string(renderingInfoType.Publisher);
                if (renderingInfoType.Keywords != null)
                    Keywords = new List<string>(renderingInfoType.Keywords);
                if (renderingInfoType.Culture != null)
                    Culture = new string(renderingInfoType.Culture);
            }

            public string? Message { get; init; }
            public string? Level { get; init; }
            public string? Opcode { get; init; }
            public string? Task { get; init; }
            public string? Channel { get; init; }
            public string? Publisher { get; init; }
            public List<string>? Keywords { get; init; }
            public string? Culture { get; init; }
        }

        public class DataType
        {
            [Key] public ulong Id { get; init; }
            public DataType() { }
            public DataType(Raw.DataType dataType)
            {
                if (dataType.Name != null)
                    Name = new string(dataType.Name);
                if (dataType.Type != null)
                    Type = new string(dataType.Type.Name);
                if (dataType.Value != null)
                    Value = new string(dataType.Value);
            }

            public string? Name { get; init; }
            public string? Type { get; init; }
            public string? Value { get; init; }
        }

        public class ComplexDataType
        {
            [Key] public ulong Id { get; init; }
            public ComplexDataType() { }
            public ComplexDataType(Raw.ComplexDataType complexDataType)
            {
                if (complexDataType != null)
                    Data = new List<DataType>(complexDataType.Data.Select(data => new DataType(data)));
                if (complexDataType != null)
                    Name = new string(complexDataType.Name);
            }

            public string? Name { get; init; }
            public List<DataType>? Data { get; init; }
        }
    }
}
