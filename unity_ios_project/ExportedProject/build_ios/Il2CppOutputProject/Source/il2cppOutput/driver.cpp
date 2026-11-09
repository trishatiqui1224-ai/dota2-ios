#include "pch-cpp.hpp"









IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END




#include "il2cpp-api.h"
#include "utils/Exception.h"
#include "utils/StringUtils.h"
#include "utils/Memory.h"
#if IL2CPP_TARGET_WINDOWS_DESKTOP
#include "Windows.h"
#include "Shellapi.h"
#elif IL2CPP_TARGET_WINDOWS_GAMES
#include "Windows.h"
#endif

extern "C" const char * platform_config_path();
extern "C" const char * platform_data_path();

static void* EmptyMalloc(size_t size){IL2CPP_ASSERT(false && "malloc/free called outside the runtime, object globally constructed?"); return nullptr; }
static void* EmptyAlignedMalloc(size_t size, size_t alignment) { IL2CPP_ASSERT(false && "malloc/free called outside the runtime, object globally constructed?"); return nullptr; }
static void EmptyFree(void* memory) { IL2CPP_ASSERT(false && "malloc/free called outside the runtime, object globally constructed?"); }
static void EmptyAlignedFree(void* memory) { IL2CPP_ASSERT(false && "malloc/free called outside the runtime, object globally constructed?"); }
static void* EmptyCalloc(size_t count, size_t size) { IL2CPP_ASSERT(false && "malloc/free called outside the runtime, object globally constructed?"); return nullptr; }
static void* EmptyRealloc(void* memory, size_t newSize) { IL2CPP_ASSERT(false && "malloc/free called outside the runtime, object globally constructed?"); return nullptr; }
static void* EmptyAlignedRealloc(void* memory, size_t newSize, size_t alignment) { IL2CPP_ASSERT(false && "malloc/free called outside the runtime, object globally constructed?"); return nullptr; }

int MainInvoker(int argc, const Il2CppNativeChar* const* argv)
{
	try
	{
		IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_missing_method_exception("Entry point not found in assembly 'DOTween, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'."), NULL);
	}
	catch (const Il2CppExceptionWrapper& e)
	{
		il2cpp_codegen_write_to_stderr("Unhandled Exception: ");
		auto method = il2cpp_class_get_method_from_name(il2cpp_object_get_class(e.ex), "ToString", 0);
		auto exceptionString = (Il2CppString*)il2cpp_runtime_invoke(method, e.ex, NULL, NULL);
		if (exceptionString != NULL)
			il2cpp_codegen_write_to_stderr(il2cpp::utils::StringUtils::Utf16ToUtf8(exceptionString->chars).c_str());
		else
			il2cpp_codegen_write_to_stderr("The exception had no message");
		#if IL2CPP_TARGET_IOS
		return 0;
		#else
		il2cpp_codegen_abort();
		il2cpp_codegen_no_return();
		#endif
	}
}

int EntryPoint(int argc, const Il2CppNativeChar* const* argv)
{
	#if IL2CPP_MONO_DEBUGGER
	#define DEBUGGER_STRINGIFY(x) #x
	#define DEBUGGER_STRINGIFY2(x) DEBUGGER_STRINGIFY(x)
	#ifdef IL2CPP_MONO_DEBUGGER_LOGFILE
	#if IL2CPP_TARGET_JAVASCRIPT || IL2CPP_TARGET_IOS
	il2cpp_debugger_set_agent_options("--debugger-agent=transport=dt_socket,address=0.0.0.0:" DEBUGGER_STRINGIFY2(IL2CPP_DEBUGGER_PORT) ",server=y,suspend=n,loglevel=9");
	#else
	il2cpp_debugger_set_agent_options("--debugger-agent=transport=dt_socket,address=0.0.0.0:" DEBUGGER_STRINGIFY2(IL2CPP_DEBUGGER_PORT) ",server=y,suspend=n,loglevel=9,logfile=" DEBUGGER_STRINGIFY2(IL2CPP_MONO_DEBUGGER_LOGFILE) "");
	#endif
	#else
	il2cpp_debugger_set_agent_options("--debugger-agent=transport=dt_socket,address=0.0.0.0:" DEBUGGER_STRINGIFY2(IL2CPP_DEBUGGER_PORT) ",server=y,suspend=n");
	#endif
	#undef DEBUGGER_STRINGIFY
	#undef DEBUGGER_STRINGIFY2
	#endif

	#if IL2CPP_DISABLE_GC
	il2cpp_gc_disable();
	#endif

#if IL2CPP_DRIVER_PLATFORM_CONFIG
	il2cpp_set_data_dir(platform_data_path());
#endif

#if IL2CPP_TARGET_WINDOWS
	il2cpp_set_commandline_arguments_utf16(argc, argv, NULL);
	il2cpp_init_utf16(argv[0]);
#else
	il2cpp_set_commandline_arguments(argc, argv, NULL);
	il2cpp_init(argv[0]);
#endif

	#if IL2CPP_TARGET_WINDOWS
	il2cpp_set_config_utf16(argv[0]);
	#elif IL2CPP_TARGET_JAVASCRIPT
	il2cpp_set_config("/");
	#else
	il2cpp_set_config(argv[0]);
	#endif


	int exitCode = MainInvoker(argc, argv);

	il2cpp_shutdown();
	Il2CppMemoryCallbacks emptyCallbacks = {EmptyMalloc, EmptyAlignedMalloc, EmptyFree, EmptyAlignedFree, EmptyCalloc, EmptyRealloc, EmptyAlignedRealloc};
	il2cpp_set_memory_callbacks(&emptyCallbacks);
	return exitCode;
}

#if IL2CPP_CUSTOM_NATIVE_ENTRYPOINT

#elif IL2CPP_TARGET_WINDOWS

#if IL2CPP_TARGET_WINDOWS_GAMES
#include <windef.h>
#include <string>
#include <locale>
#include <codecvt>
#elif !IL2CPP_TARGET_WINDOWS_DESKTOP
#include "ActivateApp.h"
#endif

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nShowCmd)
{
#if IL2CPP_TARGET_WINDOWS_DESKTOP || IL2CPP_TARGET_WINDOWS_GAMES
#if IL2CPP_TARGET_WINDOWS_DESKTOP
	int argc;
	wchar_t** argv = CommandLineToArgvW(GetCommandLineW(), &argc);
	int returnValue = EntryPoint(argc, argv);
	LocalFree(argv);
	return returnValue;
#elif IL2CPP_TARGET_WINDOWS_GAMES
	int result = EntryPoint(__argc, __wargv);
	return result;
#endif
#elif IL2CPP_WINRT_NO_ACTIVATE
	wchar_t executableName[MAX_PATH + 2];
	GetModuleFileNameW(nullptr, executableName, MAX_PATH + 2);

	int argc = 1;
	const wchar_t* argv[] = { executableName };
	return EntryPoint(argc, argv);
#else
	return WinRT::Activate(EntryPoint);
#endif
}

#else

int main(int argc, const char* argv[])
{
	return EntryPoint(argc, argv);
}

#endif
