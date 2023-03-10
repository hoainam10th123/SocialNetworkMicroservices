// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/user.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace User.Grpc.Protos {
  public static partial class UserProtoService
  {
    static readonly string __ServiceName = "UserProtoService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::User.Grpc.Protos.UserRequest> __Marshaller_UserRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::User.Grpc.Protos.UserRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::User.Grpc.Protos.LoginRequest> __Marshaller_LoginRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::User.Grpc.Protos.LoginRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::User.Grpc.Protos.UserReply> __Marshaller_UserReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::User.Grpc.Protos.UserReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::User.Grpc.Protos.Location> __Marshaller_Location = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::User.Grpc.Protos.Location.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::User.Grpc.Protos.UsersReply> __Marshaller_UsersReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::User.Grpc.Protos.UsersReply.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::User.Grpc.Protos.UserRequest, global::Google.Protobuf.WellKnownTypes.Empty> __Method_AddUser = new grpc::Method<global::User.Grpc.Protos.UserRequest, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddUser",
        __Marshaller_UserRequest,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::User.Grpc.Protos.LoginRequest, global::User.Grpc.Protos.UserReply> __Method_Login = new grpc::Method<global::User.Grpc.Protos.LoginRequest, global::User.Grpc.Protos.UserReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Login",
        __Marshaller_LoginRequest,
        __Marshaller_UserReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::User.Grpc.Protos.Location, global::User.Grpc.Protos.UsersReply> __Method_FindNearest = new grpc::Method<global::User.Grpc.Protos.Location, global::User.Grpc.Protos.UsersReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "FindNearest",
        __Marshaller_Location,
        __Marshaller_UsersReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::User.Grpc.Protos.LoginRequest, global::User.Grpc.Protos.UserReply> __Method_GetUserByUsername = new grpc::Method<global::User.Grpc.Protos.LoginRequest, global::User.Grpc.Protos.UserReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetUserByUsername",
        __Marshaller_LoginRequest,
        __Marshaller_UserReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::User.Grpc.Protos.UserReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of UserProtoService</summary>
    [grpc::BindServiceMethod(typeof(UserProtoService), "BindService")]
    public abstract partial class UserProtoServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> AddUser(global::User.Grpc.Protos.UserRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::User.Grpc.Protos.UserReply> Login(global::User.Grpc.Protos.LoginRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::User.Grpc.Protos.UsersReply> FindNearest(global::User.Grpc.Protos.Location request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::User.Grpc.Protos.UserReply> GetUserByUsername(global::User.Grpc.Protos.LoginRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(UserProtoServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_AddUser, serviceImpl.AddUser)
          .AddMethod(__Method_Login, serviceImpl.Login)
          .AddMethod(__Method_FindNearest, serviceImpl.FindNearest)
          .AddMethod(__Method_GetUserByUsername, serviceImpl.GetUserByUsername).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, UserProtoServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_AddUser, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::User.Grpc.Protos.UserRequest, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.AddUser));
      serviceBinder.AddMethod(__Method_Login, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::User.Grpc.Protos.LoginRequest, global::User.Grpc.Protos.UserReply>(serviceImpl.Login));
      serviceBinder.AddMethod(__Method_FindNearest, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::User.Grpc.Protos.Location, global::User.Grpc.Protos.UsersReply>(serviceImpl.FindNearest));
      serviceBinder.AddMethod(__Method_GetUserByUsername, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::User.Grpc.Protos.LoginRequest, global::User.Grpc.Protos.UserReply>(serviceImpl.GetUserByUsername));
    }

  }
}
#endregion
