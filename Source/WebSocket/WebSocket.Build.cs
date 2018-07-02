// Copyright 1998-2017 Epic Games, Inc. All Rights Reserved.

using System;
using System.IO;

namespace UnrealBuildTool.Rules
{
	using System.IO;

	public class WebSocket : ModuleRules
	{
		public WebSocket(ReadOnlyTargetRules Target) : base(Target)
		{
			PrivateIncludePaths.AddRange(
				new string[] {
					"WebSocket/Private",
					// ... add other private include paths required here ...
				}
				);

			PublicDependencyModuleNames.AddRange(
				new string[]
				{
					"Core",
					"Json",
					"JsonUtilities",
					"Sockets",
					// ... add other public dependencies that you statically link with here ...
				}
				);

			PrivateDependencyModuleNames.AddRange(
				new string[]
				{
					"CoreUObject",
					"Engine",
					"Slate",
					"SlateCore",
					"Json",
					"JsonUtilities",
					// ... add private dependencies that you statically link with here ...
				}
				);

			DynamicallyLoadedModuleNames.AddRange(
				new string[]
				{
					// ... add any modules that your module loads dynamically here ...
				}
				);

			string ThirdPartyDir = Path.GetFullPath(Path.Combine(ModuleDirectory, "../", "../", "ThirdParty"));

			if (Target.Platform == UnrealTargetPlatform.Win64)
			{
				PrivateDependencyModuleNames.Add("zlib");
				PrivateDependencyModuleNames.Add("OpenSSL");
				PrivateIncludePaths.Add(Path.Combine(ThirdPartyDir, "include", "Win64"));

				string strStaticPath = Path.GetFullPath(Path.Combine(ThirdPartyDir, "lib", "Win64"));
				PublicLibraryPaths.Add(strStaticPath);


				string[] StaticLibrariesX64 = new string[] {
					"websockets_static.lib",
					//"libcrypto.lib",
					//"libssl.lib",
				};

				foreach (string Lib in StaticLibrariesX64)
				{
					PublicAdditionalLibraries.Add(Lib);
				}
			}
			else if(Target.Platform == UnrealTargetPlatform.Mac)
			{
				PrivateIncludePaths.Add(Path.Combine(ThirdPartyDir, "include", "Mac"));
				string strStaticPath = Path.GetFullPath(Path.Combine(ThirdPartyDir, "lib", "Mac"));

				//PublicLibraryPaths.Add(strStaticPath);

				string[] StaticLibrariesMac = new string[] {
					"libwebsockets.a",
					"libssl.a",
					"libcrypto.a"
				};

				foreach (string Lib in StaticLibrariesMac)
				{
					PublicAdditionalLibraries.Add(Path.Combine(strStaticPath, Lib) );
				}
			}
			else if (Target.Platform == UnrealTargetPlatform.Linux)
			{
				PrivateDependencyModuleNames.Add("OpenSSL");
				PrivateIncludePaths.Add(Path.Combine(ThirdPartyDir, "include", "Linux"));

				string strStaticPath = Path.GetFullPath(Path.Combine(ThirdPartyDir, "lib", "Linux"));

				PublicLibraryPaths.Add(strStaticPath);

				string[] StaticLibrariesLinux = new string[] {
					"libwebsockets.a",
					//"libssl.a",
					//"libcrypto.a"
				};

				foreach (string Lib in StaticLibrariesLinux)
				{
					PublicAdditionalLibraries.Add(Path.Combine(strStaticPath, Lib));
				}
			}
		}
	}
}
