using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataPusherWorker
{
    public class DeviceData
    {

        [JsonProperty("receptionDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ReceptionDate { get; set; }

        [JsonProperty("ExcitationFrequency", NullValueHandling = NullValueHandling.Ignore)]
        public double? ExcitationFrequency { get; set; }

        [JsonProperty("MakeupResidualQuantityLevel", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? MakeupResidualQuantityLevel { get; set; }

        [JsonProperty("TypeName", NullValueHandling = NullValueHandling.Ignore)]
        public string TypeName { get; set; }

        [JsonProperty("InkPressure", NullValueHandling = NullValueHandling.Ignore)]
        public double? InkPressure { get; set; }

        [JsonProperty("AmbientTemperature", NullValueHandling = NullValueHandling.Ignore)]
        public long? AmbientTemperature { get; set; }

        [JsonProperty("InkMakeupInkType", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? InkMakeupInkType { get; set; }

        [JsonProperty("PrintCount", NullValueHandling = NullValueHandling.Ignore)]
        public long? PrintCount { get; set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("InkConsumption", NullValueHandling = NullValueHandling.Ignore)]
        public long? InkConsumption { get; set; }

        [JsonProperty("OperationStatus", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public string? OperationStatus { get; set; }

        [JsonProperty("PumpUseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? PumpUseTime { get; set; }

        [JsonProperty("RecoveryFilterUseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? RecoveryFilterUseTime { get; set; }

        [JsonProperty("CommunicationConnectionStatus", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CommunicationConnectionStatus { get; set; }

        [JsonProperty("CirculationFilterUseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? CirculationFilterUseTime { get; set; }

        [JsonProperty("MGVFilterUseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? MgvFilterUseTime { get; set; }

        [JsonProperty("InkOperatingTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? InkOperatingTime { get; set; }

        [JsonProperty("DeflectionVoltage", NullValueHandling = NullValueHandling.Ignore)]
        public double? DeflectionVoltage { get; set; }

        [JsonProperty("CumulativeOperationTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? CumulativeOperationTime { get; set; }

        [JsonProperty("InkFilterUseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? InkFilterUseTime { get; set; }

        [JsonProperty("CommunicatedTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CommunicatedTime { get; set; }

        [JsonProperty("HeatingUnitUseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? HeatingUnitUseTime { get; set; }

        [JsonProperty("InkViscosity", NullValueHandling = NullValueHandling.Ignore)]
        public long? InkViscosity { get; set; }

        [JsonProperty("WarningStatus", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public string? WarningStatus { get; set; }

        [JsonProperty("ExcitationV-ref", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExcitationVRef { get; set; }

        [JsonProperty("MV5UseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mv5UseTime { get; set; }

        [JsonProperty("ReceptionStatus", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? ReceptionStatus { get; set; }

        [JsonProperty("MakeupFilterUseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? MakeupFilterUseTime { get; set; }

        [JsonProperty("InkAlarmTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? InkAlarmTime { get; set; }

        [JsonProperty("InkResidualQuantityLevel", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? InkResidualQuantityLevel { get; set; }

        [JsonProperty("SerialNumber", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? SerialNumber { get; set; }

        [JsonProperty("RAirFilterUseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? RAirFilterUseTime { get; set; }

        [JsonProperty("AirFilterUseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? AirFilterUseTime { get; set; }

        [JsonProperty("MV9UseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mv9UseTime { get; set; }

        [JsonProperty("MV8UseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mv8UseTime { get; set; }

        [JsonProperty("MakeupConsumption", NullValueHandling = NullValueHandling.Ignore)]
        public long? MakeupConsumption { get; set; }

        [JsonProperty("MV3UseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mv3UseTime { get; set; }

        [JsonProperty("MV7UseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mv7UseTime { get; set; }

        [JsonProperty("MV4UseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mv4UseTime { get; set; }

        [JsonProperty("MV6UseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mv6UseTime { get; set; }

        [JsonProperty("MV2UseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mv2UseTime { get; set; }

        [JsonProperty("MV1UseTime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mv1UseTime { get; set; }

        [JsonProperty("IJPConnectionStatus", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? IjpConnectionStatus { get; set; }

        [JsonProperty("InkName", NullValueHandling = NullValueHandling.Ignore)]
        public string InkName { get; internal set; }
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

}
