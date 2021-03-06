//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: tests/protobuf-fields.proto
namespace tests.protobuf_fields
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RootElement")]
  public partial class RootElement : global::ProtoBuf.IExtensible
  {
    public RootElement() {}
    
    private string _SimpleRequired;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"SimpleRequired", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string SimpleRequired
    {
      get { return _SimpleRequired; }
      set { _SimpleRequired = value; }
    }
    private string _SimpleRequiredWithDefault;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"SimpleRequiredWithDefault", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string SimpleRequiredWithDefault
    {
      get { return _SimpleRequiredWithDefault; }
      set { _SimpleRequiredWithDefault = value; }
    }
    private ChildElement _ComplexRequired;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"ComplexRequired", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ChildElement ComplexRequired
    {
      get { return _ComplexRequired; }
      set { _ComplexRequired = value; }
    }
    private EnumerationElement _EnumeratedRequired;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"EnumeratedRequired", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public EnumerationElement EnumeratedRequired
    {
      get { return _EnumeratedRequired; }
      set { _EnumeratedRequired = value; }
    }
    private EnumerationElement _EnumeratedRequiredWithDefault;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"EnumeratedRequiredWithDefault", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public EnumerationElement EnumeratedRequiredWithDefault
    {
      get { return _EnumeratedRequiredWithDefault; }
      set { _EnumeratedRequiredWithDefault = value; }
    }
    private string _SimpleOptional = "";
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"SimpleOptional", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string SimpleOptional
    {
      get { return _SimpleOptional; }
      set { _SimpleOptional = value; }
    }
    private string _SimpleOptionalWithDefault = @"SimpleOptionalDefaultValue";
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"SimpleOptionalWithDefault", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(@"SimpleOptionalDefaultValue")]
    public string SimpleOptionalWithDefault
    {
      get { return _SimpleOptionalWithDefault; }
      set { _SimpleOptionalWithDefault = value; }
    }
    private ChildElement _ComplexOptional = null;
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"ComplexOptional", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ChildElement ComplexOptional
    {
      get { return _ComplexOptional; }
      set { _ComplexOptional = value; }
    }
    private EnumerationElement _EnumeratedOptional = EnumerationElement.FirstValue;
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"EnumeratedOptional", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(EnumerationElement.FirstValue)]
    public EnumerationElement EnumeratedOptional
    {
      get { return _EnumeratedOptional; }
      set { _EnumeratedOptional = value; }
    }
    private EnumerationElement _EnumeratedOptionalWithDefault = EnumerationElement.SecondValue;
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"EnumeratedOptionalWithDefault", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(EnumerationElement.SecondValue)]
    public EnumerationElement EnumeratedOptionalWithDefault
    {
      get { return _EnumeratedOptionalWithDefault; }
      set { _EnumeratedOptionalWithDefault = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _SimpleRepeated = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(21, Name=@"SimpleRepeated", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> SimpleRepeated
    {
      get { return _SimpleRepeated; }
    }
  
    private readonly global::System.Collections.Generic.List<ChildElement> _ComplexRepeated = new global::System.Collections.Generic.List<ChildElement>();
    [global::ProtoBuf.ProtoMember(22, Name=@"ComplexRepeated", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ChildElement> ComplexRepeated
    {
      get { return _ComplexRepeated; }
    }
  
    private readonly global::System.Collections.Generic.List<EnumerationElement> _EnumeratedRepeated = new global::System.Collections.Generic.List<EnumerationElement>();
    [global::ProtoBuf.ProtoMember(23, Name=@"EnumeratedRepeated", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<EnumerationElement> EnumeratedRepeated
    {
      get { return _EnumeratedRepeated; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ChildElement")]
  public partial class ChildElement : global::ProtoBuf.IExtensible
  {
    public ChildElement() {}
    
    private string _ChildValue;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"ChildValue", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string ChildValue
    {
      get { return _ChildValue; }
      set { _ChildValue = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"EnumerationElement")]
    public enum EnumerationElement
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"FirstValue", Value=1)]
      FirstValue = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SecondValue", Value=2)]
      SecondValue = 2
    }
  
}