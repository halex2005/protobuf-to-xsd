protobuf-to-xsd
---------------

protobuf-to-xsd is XSLT transformation to convert `FileDescriptorSet` protocol buffer to XSD schema with requirements (see [xsd-attributes.xslt](xsd-attributes.xslt)):

- use XML attributes for simple types (like strings)
- correct handling of `required` and `optional` for attributes with `use="required"` and `use="optional"` XSD attributes
- correct handling of `optional` for elements with `minOccurs="0"` attribute
- specify elements in any order (using `<xs:all />` instead of common `<xs:sequential />`)
- correct handling of `repeated` with `maxOccurs="unbounded"`

It is based on `protogen` utility written by Marc Gravell.

Usage
=====

Try it out with simple command:

```
protogen.exe -i:descriptor.proto -o:descriptor.xsd -t:xsd-attributes -d
```

Set your protocol buffer message definition file instead of `descriptor.proto`.


License
=======

[xsd-attributes.xslt](xsd-attributes.xslt) is distributes under MIT license, see license text inside file.

protogen is distributed under [Apache License, Version 2.0](protogen-license.txt).

The core Protocol Buffers technology is provided courtesy of Google.