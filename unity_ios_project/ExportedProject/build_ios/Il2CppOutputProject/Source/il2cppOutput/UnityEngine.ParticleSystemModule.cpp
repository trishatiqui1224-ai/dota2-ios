#include "pch-cpp.hpp"





template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*,const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj,invokeData.method);
	}
};

struct Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF;
struct Action_4_tA7077251D001BE8C19EBE0353750515765019B8F;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
struct Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct Delegate_t;
struct DelegateAutoCleanup_tF4041A6DFD65AFBE2B6806A7247061B73214F6A0;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct EmbeddedAttribute_t7C8E6BF2A9A8971620CB656B836C1F118BE1A2BB;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4;
struct MethodInfo_t;
struct ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1;
struct ParticleSystemRenderer_t576C271A363A738A6C576D4C6AEFB3B5B23E46C4;
struct RefSafetyRulesAttribute_tF7472D2886B69C3059066E39F5997078CFB5AC7B;
struct String_t;
struct Type_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DelegateAutoCleanup_tF4041A6DFD65AFBE2B6806A7247061B73214F6A0_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral78740424AA4FE40696B416F3EE321D6E79C3EAEE;
IL2CPP_EXTERN_C String_t* _stringLiteralEC5621377AD9B4098EC3FB34AF35BAA9E6C07B64;
IL2CPP_EXTERN_C const RuntimeMethod* ParticleSystem___AutoStaticsCleanup_Unity_Scripting_LifecycleManagement_CodeLoadedScope_Exiting_mABB866DFC7CB7A904B161D9B45F472B645CFF9AC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* CodeLoadedScope_t991AD106C7BA1C38DC99282B021A0E0986A8B811_0_0_0_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;

struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_tD4D8152B1CC10B76FF3BD3BF122F926B6BF0D3EE 
{
};
struct Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA  : public RuntimeObject
{
};
struct ClassAutoCleanup_t363F826FB3BAC8F257F45346F861A1D9BC70B419  : public RuntimeObject
{
};
struct MemberInfo_t  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Color_tD001788D726C3A7F1379BEED0260B9591F440C1F 
{
	float ___r;
	float ___g;
	float ___b;
	float ___a;
};
struct Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___rgba;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___rgba_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			uint8_t ___r;
		};
		#pragma pack(pop, tp)
		struct
		{
			uint8_t ___r_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___g_OffsetPadding[1];
			uint8_t ___g;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___g_OffsetPadding_forAlignmentOnly[1];
			uint8_t ___g_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___b_OffsetPadding[2];
			uint8_t ___b;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___b_OffsetPadding_forAlignmentOnly[2];
			uint8_t ___b_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___a_OffsetPadding[3];
			uint8_t ___a;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___a_OffsetPadding_forAlignmentOnly[3];
			uint8_t ___a_forAlignmentOnly;
		};
	};
};
struct DelegateAutoCleanup_tF4041A6DFD65AFBE2B6806A7247061B73214F6A0  : public ClassAutoCleanup_t363F826FB3BAC8F257F45346F861A1D9BC70B419
{
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ____cleanup;
	String_t* ____ownerDescription;
};
struct EmbeddedAttribute_t7C8E6BF2A9A8971620CB656B836C1F118BE1A2BB  : public Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct RefSafetyRulesAttribute_tF7472D2886B69C3059066E39F5997078CFB5AC7B  : public Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA
{
	int32_t ___Version;
};
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	float ___x;
	float ___y;
	float ___z;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct ParticleSystemCurveMode_t8BE38B5D6B050709BAE31B5556CFC3DA69FC8405 
{
	int32_t ___value__;
};
struct ParticleSystemGradientMode_t5525A27D816AC28297038E5A98856B29095C4C1A 
{
	int32_t ___value__;
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct ScopeTransitionType_t90517899E2686A0715E122C66E11ABF26A02FE8B 
{
	int32_t ___value__;
};
struct Particle_tF16C89682A98AB276CCBE4DA0A6E82F98500F79D 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Position;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Velocity;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_AnimatedVelocity;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_InitialVelocity;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_AxisOfRotation;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Rotation;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_AngularVelocity;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_StartSize;
	Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B ___m_StartColor;
	uint32_t ___m_RandomSeed;
	uint32_t ___m_ParentRandomSeed;
	float ___m_Lifetime;
	float ___m_StartLifetime;
	int32_t ___m_MeshIndex;
	float ___m_EmitAccumulator0;
	float ___m_EmitAccumulator1;
	uint32_t ___m_Flags;
};
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0 
{
	Particle_tF16C89682A98AB276CCBE4DA0A6E82F98500F79D ___m_Particle;
	bool ___m_PositionSet;
	bool ___m_VelocitySet;
	bool ___m_AxisOfRotationSet;
	bool ___m_RotationSet;
	bool ___m_AngularVelocitySet;
	bool ___m_StartSizeSet;
	bool ___m_StartColorSet;
	bool ___m_RandomSeedSet;
	bool ___m_StartLifetimeSet;
	bool ___m_MeshIndexSet;
	bool ___m_ApplyShapeToPosition;
};
struct EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshaled_pinvoke
{
	Particle_tF16C89682A98AB276CCBE4DA0A6E82F98500F79D ___m_Particle;
	int32_t ___m_PositionSet;
	int32_t ___m_VelocitySet;
	int32_t ___m_AxisOfRotationSet;
	int32_t ___m_RotationSet;
	int32_t ___m_AngularVelocitySet;
	int32_t ___m_StartSizeSet;
	int32_t ___m_StartColorSet;
	int32_t ___m_RandomSeedSet;
	int32_t ___m_StartLifetimeSet;
	int32_t ___m_MeshIndexSet;
	int32_t ___m_ApplyShapeToPosition;
};
struct EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshaled_com
{
	Particle_tF16C89682A98AB276CCBE4DA0A6E82F98500F79D ___m_Particle;
	int32_t ___m_PositionSet;
	int32_t ___m_VelocitySet;
	int32_t ___m_AxisOfRotationSet;
	int32_t ___m_RotationSet;
	int32_t ___m_AngularVelocitySet;
	int32_t ___m_StartSizeSet;
	int32_t ___m_StartColorSet;
	int32_t ___m_RandomSeedSet;
	int32_t ___m_StartLifetimeSet;
	int32_t ___m_MeshIndexSet;
	int32_t ___m_ApplyShapeToPosition;
};
struct MinMaxCurveBlittable_tF89B91FEF1792FA68E86E6B029D083A449A61755 
{
	int32_t ___m_Mode;
	float ___m_CurveMultiplier;
	intptr_t ___m_CurveMin;
	intptr_t ___m_CurveMax;
	float ___m_ConstantMin;
	float ___m_ConstantMax;
};
struct MinMaxGradientBlittable_tC20D336579526EF9AF2B98BEAF091563621E898F 
{
	int32_t ___m_Mode;
	intptr_t ___m_GradientMin;
	intptr_t ___m_GradientMax;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_ColorMin;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_ColorMax;
};
struct Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF  : public MulticastDelegate_t
{
};
struct Action_4_tA7077251D001BE8C19EBE0353750515765019B8F  : public MulticastDelegate_t
{
};
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};
struct ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};
struct Renderer_t320575F223BCB177A982E5DDB5DB19FAA89E7FBF  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};
struct ParticleSystemRenderer_t576C271A363A738A6C576D4C6AEFB3B5B23E46C4  : public Renderer_t320575F223BCB177A982E5DDB5DB19FAA89E7FBF
{
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject;
};
struct Type_t_StaticFields
{
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder;
	Il2CppChar ___Delimiter;
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes;
	RuntimeObject* ___Missing;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase;
};
struct ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields
{
	Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* ___onEmit;
	DelegateAutoCleanup_tF4041A6DFD65AFBE2B6806A7247061B73214F6A0* _____autoCleanup_Unity_Scripting_LifecycleManagement_CodeLoadedScope_Exiting;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771  : public RuntimeArray
{
	ALIGN_FIELD (8) Delegate_t* m_Items[1];

	inline Delegate_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689  : public RuntimeArray
{
	ALIGN_FIELD (8) Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* m_Items[1];

	inline Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Mesh_t6D9C539763A09BC2B12AEAEF36F6DFFC98AE63D4* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_4_Invoke_m777D49552CA89D6B58E50E97AD0C1BFF4F8C0B59_gshared_inline (Action_4_tA7077251D001BE8C19EBE0353750515765019B8F* __this, Il2CppSharedGenericObject* ___0_arg1, EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0 ___1_arg2, int32_t ___2_arg3, bool ___3_arg4, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t MarshalledUnityObject_MarshalNotNull_TisIl2CppSharedGenericObject_mCE897EC9A79983B4B1E6E2C8342452256309044A_gshared_inline (Il2CppSharedGenericObject* ___0_obj, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Attribute__ctor_m79ED1BF1EE36D1E417BA89A0D9F91F8AAD8D19E2 (Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00 (Delegate_t* ___0_a, Delegate_t* ___1_b, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3 (Delegate_t* ___0_source, Delegate_t* ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem_Emit_Internal_m54D6D9A78E8634846C9DB6445C0E0A0885E8A20E (ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1* __this, int32_t ___0_count, const RuntimeMethod* method) ;
inline void Action_4_Invoke_mD731E4C7CA8DBAD232FEB09239B9D875A0070D67_inline (Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* __this, ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1* ___0_arg1, EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0 ___1_arg2, int32_t ___2_arg3, bool ___3_arg4, const RuntimeMethod* method)
{
	((  void (*) (Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF*, ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1*, EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0, int32_t, bool, const RuntimeMethod*))Action_4_Invoke_m777D49552CA89D6B58E50E97AD0C1BFF4F8C0B59_gshared_inline)(__this, ___0_arg1, ___1_arg2, ___2_arg3, ___3_arg4, method);
}
inline intptr_t MarshalledUnityObject_MarshalNotNull_TisParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_m7A78170E3E33A80A1B66E8C5827A91794AE70589_inline (ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1*, const RuntimeMethod*))MarshalledUnityObject_MarshalNotNull_TisIl2CppSharedGenericObject_mCE897EC9A79983B4B1E6E2C8342452256309044A_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF (RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem_Emit_Internal_Injected_m2FDA9094E323EDFE3C1F5F2CE8DA7830E728556D (intptr_t ___0__unity_self, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem_remove_onEmit_mB229FA9BA0C38B05F7E242A79006687286495BA5 (Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DelegateAutoCleanup__ctor_mE4B45A8781C4FB94628512840346ADF0CEE55ABA (DelegateAutoCleanup_tF4041A6DFD65AFBE2B6806A7247061B73214F6A0* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_cleanup, Type_t* ___1_scopeType, int32_t ___2_cleanOn, String_t* ___3_ownerDescription, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SimpleThrowHelper_ThrowArgumentNullException_m7F2DC9C17105429BA3367C6956EA28B7DB7A4D58 (RuntimeObject* ___0_obj, String_t* ___1_parameterName, const RuntimeMethod* method) ;
inline intptr_t MarshalledUnityObject_MarshalNotNull_TisParticleSystemRenderer_t576C271A363A738A6C576D4C6AEFB3B5B23E46C4_mBF0A0CC5803B25DB62C394082A5813F4D14910FC_inline (ParticleSystemRenderer_t576C271A363A738A6C576D4C6AEFB3B5B23E46C4* ___0_obj, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (ParticleSystemRenderer_t576C271A363A738A6C576D4C6AEFB3B5B23E46C4*, const RuntimeMethod*))MarshalledUnityObject_MarshalNotNull_TisIl2CppSharedGenericObject_mCE897EC9A79983B4B1E6E2C8342452256309044A_gshared_inline)(___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ParticleSystemRenderer_GetMeshes_Injected_m6DADBA008313172FAE4E6150C67ADA770858C483 (intptr_t ___0__unity_self, MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689* ___1_meshes, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void EmbeddedAttribute__ctor_m7FF5B4E3620EF8A22C32F8DF43762DCC61F7E1A9 (EmbeddedAttribute_t7C8E6BF2A9A8971620CB656B836C1F118BE1A2BB* __this, const RuntimeMethod* method) 
{
	{
		Attribute__ctor_m79ED1BF1EE36D1E417BA89A0D9F91F8AAD8D19E2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RefSafetyRulesAttribute__ctor_m78F1D9F933EB9DA67BD8C2EB8F4D924BBF2974A5 (RefSafetyRulesAttribute_tF7472D2886B69C3059066E39F5997078CFB5AC7B* __this, int32_t ___0_p, const RuntimeMethod* method) 
{
	{
		Attribute__ctor_m79ED1BF1EE36D1E417BA89A0D9F91F8AAD8D19E2(__this, NULL);
		int32_t L_0 = ___0_p;
		__this->___Version = L_0;
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem_add_onEmit_mCCF090A84D1D58018DB2F01ACA70DB724BBD321A (Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit);
	Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* V_0 = NULL;
	Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* V_1 = NULL;
	Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* V_2 = NULL;
	{
		CHECKED_LOCAL_INIT(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit,(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_0 = ((ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields*)il2cpp_codegen_static_fields_for(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var))->___onEmit;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_1 = V_0;
		V_1 = L_1;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_2 = V_1;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_2, L_3, NULL);
		V_2 = ((Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF*)Castclass((RuntimeObject*)L_4, Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF_il2cpp_TypeInfo_var));
		CHECKED_LOCAL_INIT(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit,(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_5 = V_2;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_6 = V_1;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_7;
		L_7 = il2cpp_intrinsic_interlocked_compare_exchange((&((ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields*)il2cpp_codegen_static_fields_for(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var))->___onEmit), L_5, L_6);
		V_0 = L_7;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_8 = V_0;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF*)L_8) == ((RuntimeObject*)(Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem_remove_onEmit_mB229FA9BA0C38B05F7E242A79006687286495BA5 (Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit);
	Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* V_0 = NULL;
	Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* V_1 = NULL;
	Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* V_2 = NULL;
	{
		CHECKED_LOCAL_INIT(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit,(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_0 = ((ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields*)il2cpp_codegen_static_fields_for(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var))->___onEmit;
		V_0 = L_0;
	}

IL_0006:
	{
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_1 = V_0;
		V_1 = L_1;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_2 = V_1;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_2, L_3, NULL);
		V_2 = ((Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF*)Castclass((RuntimeObject*)L_4, Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF_il2cpp_TypeInfo_var));
		CHECKED_LOCAL_INIT(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit,(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_5 = V_2;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_6 = V_1;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_7;
		L_7 = il2cpp_intrinsic_interlocked_compare_exchange((&((ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields*)il2cpp_codegen_static_fields_for(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var))->___onEmit), L_5, L_6);
		V_0 = L_7;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_8 = V_0;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_9 = V_1;
		if ((!(((RuntimeObject*)(Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF*)L_8) == ((RuntimeObject*)(Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF*)L_9))))
		{
			goto IL_0006;
		}
	}
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem_Emit_m3E9E6359087607E84E0D4D35844D80D9452AD72D (ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1* __this, int32_t ___0_count, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit);
	EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* G_B2_0 = NULL;
	Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* G_B1_0 = NULL;
	{
		int32_t L_0 = ___0_count;
		ParticleSystem_Emit_Internal_m54D6D9A78E8634846C9DB6445C0E0A0885E8A20E(__this, L_0, NULL);
		CHECKED_LOCAL_INIT(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit,(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_1 = ((ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields*)il2cpp_codegen_static_fields_for(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var))->___onEmit;
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_2 = L_1;
		if (L_2)
		{
			G_B2_0 = L_2;
			goto IL_0011;
		}
		G_B1_0 = L_2;
	}
	{
		return;
	}

IL_0011:
	{
		il2cpp_codegen_initobj((&V_0), sizeof(EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0));
		EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0 L_3 = V_0;
		int32_t L_4 = ___0_count;
		NullCheck(G_B2_0);
		Action_4_Invoke_mD731E4C7CA8DBAD232FEB09239B9D875A0070D67_inline(G_B2_0, __this, L_3, L_4, (bool)0, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem_Emit_Internal_m54D6D9A78E8634846C9DB6445C0E0A0885E8A20E (ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1* __this, int32_t ___0_count, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit);
	intptr_t G_B2_0;
	memset((&G_B2_0), 0, sizeof(G_B2_0));
	intptr_t G_B1_0;
	memset((&G_B1_0), 0, sizeof(G_B1_0));
	{
		intptr_t L_0;
		L_0 = MarshalledUnityObject_MarshalNotNull_TisParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_m7A78170E3E33A80A1B66E8C5827A91794AE70589_inline(__this, NULL);
		intptr_t L_1 = L_0;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000f;
		}
		G_B1_0 = L_1;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B2_0 = G_B1_0;
	}

IL_000f:
	{
		int32_t L_2 = ___0_count;
		CHECKED_LOCAL_INIT(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit,(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		ParticleSystem_Emit_Internal_Injected_m2FDA9094E323EDFE3C1F5F2CE8DA7830E728556D(G_B2_0, L_2, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem___AutoStaticsCleanup_Unity_Scripting_LifecycleManagement_CodeLoadedScope_Exiting_mABB866DFC7CB7A904B161D9B45F472B645CFF9AC (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit);
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* V_0 = NULL;
	int32_t V_1 = 0;
	{
		CHECKED_LOCAL_INIT(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit,(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_0 = ((ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields*)il2cpp_codegen_static_fields_for(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var))->___onEmit;
		if (!L_0)
		{
			goto IL_002d;
		}
	}
	{
		CHECKED_LOCAL_INIT(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit,(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF* L_1 = ((ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields*)il2cpp_codegen_static_fields_for(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var))->___onEmit;
		NullCheck(L_1);
		DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* L_2;
		L_2 = VirtualFuncInvoker0< DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* >::Invoke(9, L_1);
		V_0 = L_2;
		V_1 = 0;
		goto IL_0027;
	}

IL_0016:
	{
		DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* L_3 = V_0;
		int32_t L_4 = V_1;
		NullCheck(L_3);
		int32_t L_5 = L_4;
		Delegate_t* L_6 = (L_3)->GetAt(static_cast<il2cpp_array_size_t>(L_5));
		CHECKED_LOCAL_INIT(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticInit,(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		ParticleSystem_remove_onEmit_mB229FA9BA0C38B05F7E242A79006687286495BA5(((Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF*)Castclass((RuntimeObject*)L_6, Action_4_t5E26F98FAE41CE494A57D595077BBC659B6334DF_il2cpp_TypeInfo_var)), NULL);
		int32_t L_7 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_7, 1));
	}

IL_0027:
	{
		int32_t L_8 = V_1;
		DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* L_9 = V_0;
		NullCheck(L_9);
		int32_t L_10 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_9)->max_length),NULL));
		if ((((int32_t)L_8) < ((int32_t)L_10)))
		{
			goto IL_0016;
		}
	}

IL_002d:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem__cctor_m621446C630A0558BA2F9D4992C9E306BFEC92A85 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DelegateAutoCleanup_tF4041A6DFD65AFBE2B6806A7247061B73214F6A0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParticleSystem___AutoStaticsCleanup_Unity_Scripting_LifecycleManagement_CodeLoadedScope_Exiting_mABB866DFC7CB7A904B161D9B45F472B645CFF9AC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral78740424AA4FE40696B416F3EE321D6E79C3EAEE);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_0, NULL, (intptr_t)((void*)ParticleSystem___AutoStaticsCleanup_Unity_Scripting_LifecycleManagement_CodeLoadedScope_Exiting_mABB866DFC7CB7A904B161D9B45F472B645CFF9AC_RuntimeMethod_var), NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_1 = { reinterpret_cast<intptr_t> (CodeLoadedScope_t991AD106C7BA1C38DC99282B021A0E0986A8B811_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_2;
		L_2 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_1, NULL);
		DelegateAutoCleanup_tF4041A6DFD65AFBE2B6806A7247061B73214F6A0* L_3 = (DelegateAutoCleanup_tF4041A6DFD65AFBE2B6806A7247061B73214F6A0*)il2cpp_codegen_object_new(DelegateAutoCleanup_tF4041A6DFD65AFBE2B6806A7247061B73214F6A0_il2cpp_TypeInfo_var);
		DelegateAutoCleanup__ctor_mE4B45A8781C4FB94628512840346ADF0CEE55ABA(L_3, L_0, L_2, 2, _stringLiteral78740424AA4FE40696B416F3EE321D6E79C3EAEE, NULL);
		((ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields*)il2cpp_codegen_static_fields_for(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var))->_____autoCleanup_Unity_Scripting_LifecycleManagement_CodeLoadedScope_Exiting = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&((ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_StaticFields*)il2cpp_codegen_static_fields_for(ParticleSystem_tB19986EE308BD63D36FB6025EEEAFBEDB97C67C1_il2cpp_TypeInfo_var))->_____autoCleanup_Unity_Scripting_LifecycleManagement_CodeLoadedScope_Exiting), (void*)L_3);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ParticleSystem_Emit_Internal_Injected_m2FDA9094E323EDFE3C1F5F2CE8DA7830E728556D (intptr_t ___0__unity_self, int32_t ___1_count, const RuntimeMethod* method) 
{
	typedef void (*ParticleSystem_Emit_Internal_Injected_m2FDA9094E323EDFE3C1F5F2CE8DA7830E728556D_ftn) (intptr_t, int32_t);
	static ParticleSystem_Emit_Internal_Injected_m2FDA9094E323EDFE3C1F5F2CE8DA7830E728556D_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (ParticleSystem_Emit_Internal_Injected_m2FDA9094E323EDFE3C1F5F2CE8DA7830E728556D_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.ParticleSystem::Emit_Internal_Injected(System.IntPtr,System.Int32)");
	_il2cpp_icall_func(___0__unity_self, ___1_count);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshal_pinvoke(const EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0& unmarshaled, EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshaled_pinvoke& marshaled)
{
	marshaled.___m_Particle = unmarshaled.___m_Particle;
	marshaled.___m_PositionSet = static_cast<int32_t>(unmarshaled.___m_PositionSet);
	marshaled.___m_VelocitySet = static_cast<int32_t>(unmarshaled.___m_VelocitySet);
	marshaled.___m_AxisOfRotationSet = static_cast<int32_t>(unmarshaled.___m_AxisOfRotationSet);
	marshaled.___m_RotationSet = static_cast<int32_t>(unmarshaled.___m_RotationSet);
	marshaled.___m_AngularVelocitySet = static_cast<int32_t>(unmarshaled.___m_AngularVelocitySet);
	marshaled.___m_StartSizeSet = static_cast<int32_t>(unmarshaled.___m_StartSizeSet);
	marshaled.___m_StartColorSet = static_cast<int32_t>(unmarshaled.___m_StartColorSet);
	marshaled.___m_RandomSeedSet = static_cast<int32_t>(unmarshaled.___m_RandomSeedSet);
	marshaled.___m_StartLifetimeSet = static_cast<int32_t>(unmarshaled.___m_StartLifetimeSet);
	marshaled.___m_MeshIndexSet = static_cast<int32_t>(unmarshaled.___m_MeshIndexSet);
	marshaled.___m_ApplyShapeToPosition = static_cast<int32_t>(unmarshaled.___m_ApplyShapeToPosition);
}
IL2CPP_EXTERN_C void EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshal_pinvoke_back(const EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshaled_pinvoke& marshaled, EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0& unmarshaled)
{
	Particle_tF16C89682A98AB276CCBE4DA0A6E82F98500F79D unmarshaledm_Particle_temp_0;
	memset((&unmarshaledm_Particle_temp_0), 0, sizeof(unmarshaledm_Particle_temp_0));
	unmarshaledm_Particle_temp_0 = marshaled.___m_Particle;
	unmarshaled.___m_Particle = unmarshaledm_Particle_temp_0;
	bool unmarshaledm_PositionSet_temp_1 = false;
	unmarshaledm_PositionSet_temp_1 = static_cast<bool>(marshaled.___m_PositionSet);
	unmarshaled.___m_PositionSet = unmarshaledm_PositionSet_temp_1;
	bool unmarshaledm_VelocitySet_temp_2 = false;
	unmarshaledm_VelocitySet_temp_2 = static_cast<bool>(marshaled.___m_VelocitySet);
	unmarshaled.___m_VelocitySet = unmarshaledm_VelocitySet_temp_2;
	bool unmarshaledm_AxisOfRotationSet_temp_3 = false;
	unmarshaledm_AxisOfRotationSet_temp_3 = static_cast<bool>(marshaled.___m_AxisOfRotationSet);
	unmarshaled.___m_AxisOfRotationSet = unmarshaledm_AxisOfRotationSet_temp_3;
	bool unmarshaledm_RotationSet_temp_4 = false;
	unmarshaledm_RotationSet_temp_4 = static_cast<bool>(marshaled.___m_RotationSet);
	unmarshaled.___m_RotationSet = unmarshaledm_RotationSet_temp_4;
	bool unmarshaledm_AngularVelocitySet_temp_5 = false;
	unmarshaledm_AngularVelocitySet_temp_5 = static_cast<bool>(marshaled.___m_AngularVelocitySet);
	unmarshaled.___m_AngularVelocitySet = unmarshaledm_AngularVelocitySet_temp_5;
	bool unmarshaledm_StartSizeSet_temp_6 = false;
	unmarshaledm_StartSizeSet_temp_6 = static_cast<bool>(marshaled.___m_StartSizeSet);
	unmarshaled.___m_StartSizeSet = unmarshaledm_StartSizeSet_temp_6;
	bool unmarshaledm_StartColorSet_temp_7 = false;
	unmarshaledm_StartColorSet_temp_7 = static_cast<bool>(marshaled.___m_StartColorSet);
	unmarshaled.___m_StartColorSet = unmarshaledm_StartColorSet_temp_7;
	bool unmarshaledm_RandomSeedSet_temp_8 = false;
	unmarshaledm_RandomSeedSet_temp_8 = static_cast<bool>(marshaled.___m_RandomSeedSet);
	unmarshaled.___m_RandomSeedSet = unmarshaledm_RandomSeedSet_temp_8;
	bool unmarshaledm_StartLifetimeSet_temp_9 = false;
	unmarshaledm_StartLifetimeSet_temp_9 = static_cast<bool>(marshaled.___m_StartLifetimeSet);
	unmarshaled.___m_StartLifetimeSet = unmarshaledm_StartLifetimeSet_temp_9;
	bool unmarshaledm_MeshIndexSet_temp_10 = false;
	unmarshaledm_MeshIndexSet_temp_10 = static_cast<bool>(marshaled.___m_MeshIndexSet);
	unmarshaled.___m_MeshIndexSet = unmarshaledm_MeshIndexSet_temp_10;
	bool unmarshaledm_ApplyShapeToPosition_temp_11 = false;
	unmarshaledm_ApplyShapeToPosition_temp_11 = static_cast<bool>(marshaled.___m_ApplyShapeToPosition);
	unmarshaled.___m_ApplyShapeToPosition = unmarshaledm_ApplyShapeToPosition_temp_11;
}
IL2CPP_EXTERN_C void EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshal_pinvoke_cleanup(EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshaled_pinvoke& marshaled)
{
}
IL2CPP_EXTERN_C void EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshal_com(const EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0& unmarshaled, EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshaled_com& marshaled)
{
	marshaled.___m_Particle = unmarshaled.___m_Particle;
	marshaled.___m_PositionSet = static_cast<int32_t>(unmarshaled.___m_PositionSet);
	marshaled.___m_VelocitySet = static_cast<int32_t>(unmarshaled.___m_VelocitySet);
	marshaled.___m_AxisOfRotationSet = static_cast<int32_t>(unmarshaled.___m_AxisOfRotationSet);
	marshaled.___m_RotationSet = static_cast<int32_t>(unmarshaled.___m_RotationSet);
	marshaled.___m_AngularVelocitySet = static_cast<int32_t>(unmarshaled.___m_AngularVelocitySet);
	marshaled.___m_StartSizeSet = static_cast<int32_t>(unmarshaled.___m_StartSizeSet);
	marshaled.___m_StartColorSet = static_cast<int32_t>(unmarshaled.___m_StartColorSet);
	marshaled.___m_RandomSeedSet = static_cast<int32_t>(unmarshaled.___m_RandomSeedSet);
	marshaled.___m_StartLifetimeSet = static_cast<int32_t>(unmarshaled.___m_StartLifetimeSet);
	marshaled.___m_MeshIndexSet = static_cast<int32_t>(unmarshaled.___m_MeshIndexSet);
	marshaled.___m_ApplyShapeToPosition = static_cast<int32_t>(unmarshaled.___m_ApplyShapeToPosition);
}
IL2CPP_EXTERN_C void EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshal_com_back(const EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshaled_com& marshaled, EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0& unmarshaled)
{
	Particle_tF16C89682A98AB276CCBE4DA0A6E82F98500F79D unmarshaledm_Particle_temp_0;
	memset((&unmarshaledm_Particle_temp_0), 0, sizeof(unmarshaledm_Particle_temp_0));
	unmarshaledm_Particle_temp_0 = marshaled.___m_Particle;
	unmarshaled.___m_Particle = unmarshaledm_Particle_temp_0;
	bool unmarshaledm_PositionSet_temp_1 = false;
	unmarshaledm_PositionSet_temp_1 = static_cast<bool>(marshaled.___m_PositionSet);
	unmarshaled.___m_PositionSet = unmarshaledm_PositionSet_temp_1;
	bool unmarshaledm_VelocitySet_temp_2 = false;
	unmarshaledm_VelocitySet_temp_2 = static_cast<bool>(marshaled.___m_VelocitySet);
	unmarshaled.___m_VelocitySet = unmarshaledm_VelocitySet_temp_2;
	bool unmarshaledm_AxisOfRotationSet_temp_3 = false;
	unmarshaledm_AxisOfRotationSet_temp_3 = static_cast<bool>(marshaled.___m_AxisOfRotationSet);
	unmarshaled.___m_AxisOfRotationSet = unmarshaledm_AxisOfRotationSet_temp_3;
	bool unmarshaledm_RotationSet_temp_4 = false;
	unmarshaledm_RotationSet_temp_4 = static_cast<bool>(marshaled.___m_RotationSet);
	unmarshaled.___m_RotationSet = unmarshaledm_RotationSet_temp_4;
	bool unmarshaledm_AngularVelocitySet_temp_5 = false;
	unmarshaledm_AngularVelocitySet_temp_5 = static_cast<bool>(marshaled.___m_AngularVelocitySet);
	unmarshaled.___m_AngularVelocitySet = unmarshaledm_AngularVelocitySet_temp_5;
	bool unmarshaledm_StartSizeSet_temp_6 = false;
	unmarshaledm_StartSizeSet_temp_6 = static_cast<bool>(marshaled.___m_StartSizeSet);
	unmarshaled.___m_StartSizeSet = unmarshaledm_StartSizeSet_temp_6;
	bool unmarshaledm_StartColorSet_temp_7 = false;
	unmarshaledm_StartColorSet_temp_7 = static_cast<bool>(marshaled.___m_StartColorSet);
	unmarshaled.___m_StartColorSet = unmarshaledm_StartColorSet_temp_7;
	bool unmarshaledm_RandomSeedSet_temp_8 = false;
	unmarshaledm_RandomSeedSet_temp_8 = static_cast<bool>(marshaled.___m_RandomSeedSet);
	unmarshaled.___m_RandomSeedSet = unmarshaledm_RandomSeedSet_temp_8;
	bool unmarshaledm_StartLifetimeSet_temp_9 = false;
	unmarshaledm_StartLifetimeSet_temp_9 = static_cast<bool>(marshaled.___m_StartLifetimeSet);
	unmarshaled.___m_StartLifetimeSet = unmarshaledm_StartLifetimeSet_temp_9;
	bool unmarshaledm_MeshIndexSet_temp_10 = false;
	unmarshaledm_MeshIndexSet_temp_10 = static_cast<bool>(marshaled.___m_MeshIndexSet);
	unmarshaled.___m_MeshIndexSet = unmarshaledm_MeshIndexSet_temp_10;
	bool unmarshaledm_ApplyShapeToPosition_temp_11 = false;
	unmarshaledm_ApplyShapeToPosition_temp_11 = static_cast<bool>(marshaled.___m_ApplyShapeToPosition);
	unmarshaled.___m_ApplyShapeToPosition = unmarshaledm_ApplyShapeToPosition_temp_11;
}
IL2CPP_EXTERN_C void EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshal_com_cleanup(EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0_marshaled_com& marshaled)
{
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ParticleSystemRenderer_GetMeshes_m3CA9AA8947C7F0468F6C0B7F1344D747EA43D440 (ParticleSystemRenderer_t576C271A363A738A6C576D4C6AEFB3B5B23E46C4* __this, MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689* ___0_meshes, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralEC5621377AD9B4098EC3FB34AF35BAA9E6C07B64);
		s_Il2CppMethodInitialized = true;
	}
	intptr_t G_B4_0;
	memset((&G_B4_0), 0, sizeof(G_B4_0));
	intptr_t G_B3_0;
	memset((&G_B3_0), 0, sizeof(G_B3_0));
	{
		MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689* L_0 = ___0_meshes;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689* L_1 = ___0_meshes;
		SimpleThrowHelper_ThrowArgumentNullException_m7F2DC9C17105429BA3367C6956EA28B7DB7A4D58((RuntimeObject*)L_1, _stringLiteralEC5621377AD9B4098EC3FB34AF35BAA9E6C07B64, NULL);
	}

IL_000e:
	{
		intptr_t L_2;
		L_2 = MarshalledUnityObject_MarshalNotNull_TisParticleSystemRenderer_t576C271A363A738A6C576D4C6AEFB3B5B23E46C4_mBF0A0CC5803B25DB62C394082A5813F4D14910FC_inline(__this, NULL);
		intptr_t L_3 = L_2;
		if (L_3)
		{
			G_B4_0 = L_3;
			goto IL_001e;
		}
		G_B3_0 = L_3;
	}
	{
		ThrowHelper_ThrowNullReferenceException_mA9C7629D32240EE0218631933DAC647668CA63CF(__this, NULL);
		G_B4_0 = G_B3_0;
	}

IL_001e:
	{
		MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689* L_4 = ___0_meshes;
		int32_t L_5;
		L_5 = ParticleSystemRenderer_GetMeshes_Injected_m6DADBA008313172FAE4E6150C67ADA770858C483(G_B4_0, L_4, NULL);
		return L_5;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ParticleSystemRenderer_GetMeshes_Injected_m6DADBA008313172FAE4E6150C67ADA770858C483 (intptr_t ___0__unity_self, MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689* ___1_meshes, const RuntimeMethod* method) 
{
	typedef int32_t (*ParticleSystemRenderer_GetMeshes_Injected_m6DADBA008313172FAE4E6150C67ADA770858C483_ftn) (intptr_t, MeshU5BU5D_t178CA36422FC397211E68FB7E39C5B2F95619689*);
	static ParticleSystemRenderer_GetMeshes_Injected_m6DADBA008313172FAE4E6150C67ADA770858C483_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (ParticleSystemRenderer_GetMeshes_Injected_m6DADBA008313172FAE4E6150C67ADA770858C483_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.ParticleSystemRenderer::GetMeshes_Injected(System.IntPtr,UnityEngine.Mesh[])");
	int32_t icallRetVal = _il2cpp_icall_func(___0__unity_self, ___1_meshes);
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_4_Invoke_m777D49552CA89D6B58E50E97AD0C1BFF4F8C0B59_gshared_inline (Action_4_tA7077251D001BE8C19EBE0353750515765019B8F* __this, Il2CppSharedGenericObject* ___0_arg1, EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0 ___1_arg2, int32_t ___2_arg3, bool ___3_arg4, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, Il2CppSharedGenericObject*, EmitParams_tE76279CE754C7B0A4ECDA7E294587AACB039FBA0, int32_t, bool, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_arg1, ___1_arg2, ___2_arg3, ___3_arg4, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t MarshalledUnityObject_MarshalNotNull_TisIl2CppSharedGenericObject_mCE897EC9A79983B4B1E6E2C8342452256309044A_gshared_inline (Il2CppSharedGenericObject* ___0_obj, const RuntimeMethod* method) 
{
	{
		Il2CppSharedGenericObject* L_0 = ___0_obj;
		NullCheck(L_0);
		intptr_t L_1 = ((Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)L_0)->___m_CachedPtr;
		return L_1;
	}
}
