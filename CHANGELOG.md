# [2.2.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v2.2.0-beta.1...v2.2.0-beta.2) (2026-07-21)


### Bug Fixes

* **connection:** escape Spectre markup brackets in deprecation descriptions ([5379d5f](https://github.com/DIGITALLNature/DigitallPower/commit/5379d5fdaceee46ab9c54de0c090a05bd714bc7a))
* **connection:** rename type labels from Token/Classic to MSAL/ConnectionString ([159920f](https://github.com/DIGITALLNature/DigitallPower/commit/159920f9193cdac4be2f2ed4f4e22566d21822b0))
* dont persist profile when connection fails ([7c122e1](https://github.com/DIGITALLNature/DigitallPower/commit/7c122e1a078fe223db1c93b811dddf7b525a0763))
* qoadana issue ([b6ed909](https://github.com/DIGITALLNature/DigitallPower/commit/b6ed9097dc9a5ddbb17845a8d5e63c2f80845bf5))
* resolve new qodana issues ([c3a63a2](https://github.com/DIGITALLNature/DigitallPower/commit/c3a63a2b5452f02dfcd0233eb2c6cd998652dfb5))
* resolve qodana issues ([384f653](https://github.com/DIGITALLNature/DigitallPower/commit/384f653d9bc6d58a09b1cb17c80013a12b65d37c))
* throw ArgumentNullException instead of Debug.Assert ([7ad7216](https://github.com/DIGITALLNature/DigitallPower/commit/7ad7216e775bc71ed8b237eb5d10a73c5fc46f2c))


### Features

* **connection:** add confirmation prompt to delete --all ([2deceef](https://github.com/DIGITALLNature/DigitallPower/commit/2deceef8b6198601539341382e26e7747a17d1f7))
* **connection:** add Validate() to CreateConnectionSettings for mutual exclusivity ([06bf182](https://github.com/DIGITALLNature/DigitallPower/commit/06bf182a207f5f17ef22d9a7ba50924ffe320dc5))
* **connection:** introduce connection command, deprecate profile ([5635d32](https://github.com/DIGITALLNature/DigitallPower/commit/5635d326df1fc236d4787fd318c50df2ec9c323a))

# [2.2.0-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0...v2.2.0-beta.1) (2026-07-21)


### Features

* **telemetry:** anonymize org URLs and home paths, record exceptions as OTel events ([4889812](https://github.com/DIGITALLNature/DigitallPower/commit/4889812384f93a9b9615a9fe9df6d56b4a3ef76f))

# [2.1.0-beta.61](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.60...v2.1.0-beta.61) (2026-07-03)


### Features

* **telemetry:** anonymize org URLs and home paths, record exceptions as OTel events ([4889812](https://github.com/DIGITALLNature/DigitallPower/commit/4889812384f93a9b9615a9fe9df6d56b4a3ef76f))

# [2.1.0](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0...v2.1.0) (2026-07-01)


### Bug Fixes

* add necessary test data ([3e4b373](https://github.com/DIGITALLNature/DigitallPower/commit/3e4b37396f08ac335ad5f7a479b326afe5aa7504))
* **codegeneration:** add null guards and use concrete return types in MetadataService ([3d1fa16](https://github.com/DIGITALLNature/DigitallPower/commit/3d1fa160c8c9798363b224ee4a1d5d45a783ef8e))
* **codegeneration:** ditch json polymorphic ([c84c1ec](https://github.com/DIGITALLNature/DigitallPower/commit/c84c1ecc4c63e11a71e1784eedc5b3553899da3d))
* **codegeneration:** eliminate empty lines in Entity.dotnet.liquid for-loops ([27f4008](https://github.com/DIGITALLNature/DigitallPower/commit/27f40084e1d121e1e6da739919ee302a3dbbd9dd))
* **codegeneration:** normalize all dotnet Liquid templates to 4 spaces ([3487078](https://github.com/DIGITALLNature/DigitallPower/commit/34870781b88671802f45821399315c940e257531))
* **codegeneration:** polymorphic json config deserialization ([063163a](https://github.com/DIGITALLNature/DigitallPower/commit/063163a97376058ff96d1713860d0bc4a70ea0ad))
* **codegeneration:** propagate worker failure to exit code ([2a15cb3](https://github.com/DIGITALLNature/DigitallPower/commit/2a15cb3fdfdd71d56f39a61a284a2b597e0c5a3d))
* **codegeneration:** resolve covariant array conversion warnings in MetadataService ([543545e](https://github.com/DIGITALLNature/DigitallPower/commit/543545edd5514e0a927eb9c60d37e99191bd32e3))
* complete TUnit assertion API migration ([b4ebb8e](https://github.com/DIGITALLNature/DigitallPower/commit/b4ebb8e93a27e5ccfffa3f96bc320f94acb9bf99))
* correct TUnit Count() API usage - use IsEqualTo not EqualTo ([e318a0f](https://github.com/DIGITALLNature/DigitallPower/commit/e318a0f700fd9f7dae07d1ba1e2c31412c6ddd30))
* dto properties formatting ([a664ded](https://github.com/DIGITALLNature/DigitallPower/commit/a664ded55a92cf2d9cfbd873b0064a2264a3d163))
* escape markup for component name ([#105](https://github.com/DIGITALLNature/DigitallPower/issues/105)) ([4d9bf34](https://github.com/DIGITALLNature/DigitallPower/commit/4d9bf341ada770a1e031a740327468f48c97a340))
* extend known namespaces list ([2cd1174](https://github.com/DIGITALLNature/DigitallPower/commit/2cd1174481284222ae2ad8280ccf12cf6f0ca25b))
* incompatible update of Microsoft.AspNetCore.DataProtection ([4c75604](https://github.com/DIGITALLNature/DigitallPower/commit/4c75604c6275951ea809a894408974f1e1cc5462))
* link plugin to custom api ([#100](https://github.com/DIGITALLNature/DigitallPower/issues/100)) ([56a1d2a](https://github.com/DIGITALLNature/DigitallPower/commit/56a1d2abb007999af0305a964a0119b89bcb7e4b))
* **maintenance workflowstate:** assign workflows before enabling then  Mitigates issues with licensing when old owner is no longer licensed ([d1da4c5](https://github.com/DIGITALLNature/DigitallPower/commit/d1da4c5bd33d5b8a0ca8569de3f6a2b2b09b6587))
* migrate TUnit assertions from HasCount() to Count() ([157dd99](https://github.com/DIGITALLNature/DigitallPower/commit/157dd99784e9d16f6d96b9d9cd80456e220519b0))
* plugin package upgrade ([1454086](https://github.com/DIGITALLNature/DigitallPower/commit/1454086514a087f91da16a98168a6b80955b081d))
* problem with adding a dependent plugin to a solution ([5f8696e](https://github.com/DIGITALLNature/DigitallPower/commit/5f8696e86d87d460052b79bef0dedd960c6d4247))
* **push:** add existing webresources to solution on update and up2date ([829b9be](https://github.com/DIGITALLNature/DigitallPower/commit/829b9be0a1bbcccd010deed5e13a378bff862abe))
* **push:** address review feedback - migrate Custom APIs for PowerPlugins, use FirstOrDefault ([e4e55b7](https://github.com/DIGITALLNature/DigitallPower/commit/e4e55b7ee0a2e570e1b6c59034c99337997cdd3f))
* **push:** change ExecutionOrder default from 1 to 100 ([5e580a4](https://github.com/DIGITALLNature/DigitallPower/commit/5e580a4b308dd515b68fc34d7d4bdd2c94ae8eab))
* **push:** rename managed identity assembly linker method ([d774569](https://github.com/DIGITALLNature/DigitallPower/commit/d7745695fc89d36fc1e3edde9c5c63781791b86c))
* **push:** resolve analyzer warnings in AssemblyProcessor and migration tests ([2d12095](https://github.com/DIGITALLNature/DigitallPower/commit/2d12095a1d6eb1a8b1b71b8c7708415807a0b96a))
* replace Thread.Sleep with Task.Delay in push-adjacent modules ([bd9ab75](https://github.com/DIGITALLNature/DigitallPower/commit/bd9ab756fc84db8958b963e9d111dcbbb5014599)), closes [#78](https://github.com/DIGITALLNature/DigitallPower/issues/78)
* resolve high-priority code analysis warnings ([f71be1f](https://github.com/DIGITALLNature/DigitallPower/commit/f71be1f67a65f88363ea0e886bdda9e6bded4380)), closes [hi#priority](https://github.com/hi/issues/priority)
* resolve new Qodana findings in codegeneration and push modules ([3f20522](https://github.com/DIGITALLNature/DigitallPower/commit/3f2052290f82acf33436a4228f6944afd2850bda)), closes [#region](https://github.com/DIGITALLNature/DigitallPower/issues/region)
* resolve Qodana static analysis findings ([58b5a26](https://github.com/DIGITALLNature/DigitallPower/commit/58b5a26efa9f6749d7f9a5c202bc967e3dfc770c))
* sign test assemblies and build/test exclusively in Release ([3236382](https://github.com/DIGITALLNature/DigitallPower/commit/3236382cafb96d6efc01a6a5f2dc5cd55dca62b5)), closes [#if](https://github.com/DIGITALLNature/DigitallPower/issues/if)
* **telemetry:** align env var name to DGT_TELEMETRY_CONNECTION_STRING ([12f8521](https://github.com/DIGITALLNature/DigitallPower/commit/12f852188201239d255b6cfef64ce20b4e1834e6))
* **tests:** adapt to regenerated Workflow.Options.Category naming ([1baa966](https://github.com/DIGITALLNature/DigitallPower/commit/1baa9664ab1c481892f9658f5cf7244bc17d28a4))
* **test:** update Digitall.Dataverse.Testing to beta.6 and fix BulkDelete test ([007f20f](https://github.com/DIGITALLNature/DigitallPower/commit/007f20f41ebb05883c4017a5e18db5d282bf286d))
* **tsl:** add delegate overload to form control collection ([24a8bbc](https://github.com/DIGITALLNature/DigitallPower/commit/24a8bbc254e7a3e0610d828f5c67958ebdae289f))
* **tsl:** correctly skip form generation for bpf entities ([2d3fa95](https://github.com/DIGITALLNature/DigitallPower/commit/2d3fa95f001ebb2a4a5f9c6a7a6735599cbe422e))
* **tsl:** fix broken Entity enum ([ce136dc](https://github.com/DIGITALLNature/DigitallPower/commit/ce136dc432c8689403f7c1910149828bee211da6))
* **tsl:** fix inlining of entity logical name constant ([16a8a01](https://github.com/DIGITALLNature/DigitallPower/commit/16a8a01f838c3db5d22b106680be50901661b811))
* **tsl:** graceful fallback when no localized label found ([9de4a6a](https://github.com/DIGITALLNature/DigitallPower/commit/9de4a6a0e623a44ca402da08f8a2a485cad97e74))
* **tsl:** proper default value when Attributemetadata is nowhere to be found ([7af645e](https://github.com/DIGITALLNature/DigitallPower/commit/7af645e82ef6f070fce6c09c02d22050e5dffded))
* **tsl:** resolving of Attribute via logicalname in filters now returns generic one when not found ([809568a](https://github.com/DIGITALLNature/DigitallPower/commit/809568a3d65d62e9f7d37427a991ed26575a407e))
* **tsl:** use unique for solution form creation ([#114](https://github.com/DIGITALLNature/DigitallPower/issues/114)) ([f78579c](https://github.com/DIGITALLNature/DigitallPower/commit/f78579c02440a880361872858570ee8756207885))
* update Digitall.Dataverse.Testing to beta.8 and fix telemetry env var typo ([a1906fd](https://github.com/DIGITALLNature/DigitallPower/commit/a1906fd1e2a089907b09e85fdc0deddd0610eb69))
* update dotnet version mentioned ([af0dd1c](https://github.com/DIGITALLNature/DigitallPower/commit/af0dd1c8c9e8b46a7a180b16890a565424fdd3c4))
* use home account identifier for msal token ([b05ac7c](https://github.com/DIGITALLNature/DigitallPower/commit/b05ac7c7d9c2fab555dcae1205c9df213dae8479))


### Features

* add detailed option to create workflowstate config ([aca08f2](https://github.com/DIGITALLNature/DigitallPower/commit/aca08f2c963c1404e5cd31d1c637b9aa617fe772))
* add dotnet-suggest tab completion support ([0ebcf44](https://github.com/DIGITALLNature/DigitallPower/commit/0ebcf4434fe78c6d6f688d797074502ec0849cca))
* add msal example ([fb594d7](https://github.com/DIGITALLNature/DigitallPower/commit/fb594d7140b684fe12668251309e4e350c4b1645))
* add OpenTelemetry usage telemetry with opt-out ([4676f12](https://github.com/DIGITALLNature/DigitallPower/commit/4676f1228163d6ee6250f4bd81f03ae113ecd8fc))
* add semi global profile parameter to run commands against specific profile ([#102](https://github.com/DIGITALLNature/DigitallPower/issues/102)) ([e368247](https://github.com/DIGITALLNature/DigitallPower/commit/e3682472e5878ca7047252ef26e8740c6d92b77a)), closes [#103](https://github.com/DIGITALLNature/DigitallPower/issues/103)
* assembly with major version change should replace older version and allow purge of outdated ([6ded506](https://github.com/DIGITALLNature/DigitallPower/commit/6ded5068c1a63b229425c305f0879ce50c8ddd55))
* **codegeneration:** redesign V2 config with scope/output separation ([09880c8](https://github.com/DIGITALLNature/DigitallPower/commit/09880c8940adab7cff1250f7c00913aba828d61c))
* **codegeneration:** remove brownout warnings for Actions, AdditionalSdkMessages and CustomAPIs ([0c58601](https://github.com/DIGITALLNature/DigitallPower/commit/0c58601948d6ebc34907de14103ff6400121e4b6)), closes [#89](https://github.com/DIGITALLNature/DigitallPower/issues/89)
* **common:** add RoutingRuleItem partial extension for msdyn_routeto and assignobjectidtype ([9779b32](https://github.com/DIGITALLNature/DigitallPower/commit/9779b326b43b99f6c9bf90799316d45a57f875cd))
* **complete:** add dgtp complete setup and install-shell commands ([0b58e2b](https://github.com/DIGITALLNature/DigitallPower/commit/0b58e2b0c9079dc3263c7dfa840df23290a8fe5e))
* **completion:** add dynamic profile name completions ([7841d58](https://github.com/DIGITALLNature/DigitallPower/commit/7841d586524d4641a44ccac2f94adb4f8cea2cf9))
* lighter typescript model generation ([#79](https://github.com/DIGITALLNature/DigitallPower/issues/79)) ([978678b](https://github.com/DIGITALLNature/DigitallPower/commit/978678bea4b2babfdbaef1137f2aae40d96d0c07))
* lightweight model generation for TypeScript ([992c8c7](https://github.com/DIGITALLNature/DigitallPower/commit/992c8c758bf914667f9f27e630640771f8ab2edf))
* **profile:** add non-interactive auth mode for coding agents ([#157](https://github.com/DIGITALLNature/DigitallPower/issues/157)) ([f92e336](https://github.com/DIGITALLNature/DigitallPower/commit/f92e336b87a1400630eabd52094b9d4ddcfab684))
* **push:** evaluate CustomDataProviderRegistrationAttribute for step generation ([6c169b0](https://github.com/DIGITALLNature/DigitallPower/commit/6c169b0cc0c2e349734300fe50fd70d80fc2c561))
* **push:** evaluate ManagedIdentityRegistrationAttribute for identity linking ([68de0b2](https://github.com/DIGITALLNature/DigitallPower/commit/68de0b2e9c17792a195d97b54f0a82f13dc22d1c))
* **push:** migrate plugin steps and Custom API references on assembly version upgrade ([93a717f](https://github.com/DIGITALLNature/DigitallPower/commit/93a717f2e5c943469ff0e1b1920ca3190ac4464a)), closes [#91](https://github.com/DIGITALLNature/DigitallPower/issues/91)
* **telemetry:** embed connection string at build time via AssemblyMetadata ([8003a80](https://github.com/DIGITALLNature/DigitallPower/commit/8003a808e5af233ab2058c8ad05935f03ea90c1c))
* **telemetry:** expose beta version in activity source ([c055f0e](https://github.com/DIGITALLNature/DigitallPower/commit/c055f0e6ba3a9ecc423ecd9a4dc6bec868b47fe6))
* treat macro as workflow with normal name ([6e6a8db](https://github.com/DIGITALLNature/DigitallPower/commit/6e6a8db8dc1952747f5092ab479f303050412876))
* **tsl:** adapt logic so ts files have a folder structure ([#120](https://github.com/DIGITALLNature/DigitallPower/issues/120)) ([d3a70f6](https://github.com/DIGITALLNature/DigitallPower/commit/d3a70f6f8ff48af31b201bc26cbe764ed4c8b3b2))
* **tsl:** add additional extra mock function in unit test builder ([#138](https://github.com/DIGITALLNature/DigitallPower/issues/138)) ([e0daf8c](https://github.com/DIGITALLNature/DigitallPower/commit/e0daf8cacc0c91fc476d7190bfd18a70076ecd15))
* **tsl:** add extra test mock util functions ([#135](https://github.com/DIGITALLNature/DigitallPower/issues/135)) ([d1be7d0](https://github.com/DIGITALLNature/DigitallPower/commit/d1be7d0625646c312bbe3a7f0ac829c99daa13a3))
* **tsl:** add hierarchical namespaces to the form and entities ([#121](https://github.com/DIGITALLNature/DigitallPower/issues/121)) ([4ce3692](https://github.com/DIGITALLNature/DigitallPower/commit/4ce3692c59cfb4ef27600b2e8105113f9e2f6526))
* **tsl:** add quick view mapping sort header process controls ([#118](https://github.com/DIGITALLNature/DigitallPower/issues/118)) ([28e06d4](https://github.com/DIGITALLNature/DigitallPower/commit/28e06d4688e080f237bcf039c148f22359f9864f))
* **tsl:** add small extra corrections to the unit tests utils ([#141](https://github.com/DIGITALLNature/DigitallPower/issues/141)) ([7734377](https://github.com/DIGITALLNature/DigitallPower/commit/7734377f005f3ad6aab93f88fcdc57dcec85e477))
* **tsl:** add type as branding property to distinguish them ([#134](https://github.com/DIGITALLNature/DigitallPower/issues/134)) ([108c468](https://github.com/DIGITALLNature/DigitallPower/commit/108c468d2f7fae622b4f5946239bd83b4d3bc85e))
* **tsl:** add Typescript Light DTO generation for Entites ([3c66950](https://github.com/DIGITALLNature/DigitallPower/commit/3c6695093282dd5f591bfa23df978b523a1efa16))
* **tsl:** added extra handling for lookup with multiple targets polymorphic ones ([#130](https://github.com/DIGITALLNature/DigitallPower/issues/130)) ([43634ce](https://github.com/DIGITALLNature/DigitallPower/commit/43634cecbbd51da5fefaea38a0d8a985de1f9311))
* **tsl:** correct issue with the generation of control form when name not attribute ([#137](https://github.com/DIGITALLNature/DigitallPower/issues/137)) ([8b6cde7](https://github.com/DIGITALLNature/DigitallPower/commit/8b6cde766f1fcab261221b40218c7b77788ebcef))
* **tsl:** correct xrm-type class in template ([#143](https://github.com/DIGITALLNature/DigitallPower/issues/143)) ([149df5a](https://github.com/DIGITALLNature/DigitallPower/commit/149df5adaa4744aeec3fdac14e3c9504813a2fb0))
* **tsl:** create initial test helper classes ([#125](https://github.com/DIGITALLNature/DigitallPower/issues/125)) ([884f934](https://github.com/DIGITALLNature/DigitallPower/commit/884f934d6ddc4199304d7ba9f7cbf03bd2079336))
* **tsl:** enhance generated types ([#107](https://github.com/DIGITALLNature/DigitallPower/issues/107)) ([8cdd380](https://github.com/DIGITALLNature/DigitallPower/commit/8cdd38093f8181f0b8466cd74640c5b06b26c2ce))
* **tsl:** extra adaptations for ts custom api generation ([#119](https://github.com/DIGITALLNature/DigitallPower/issues/119)) ([79e1735](https://github.com/DIGITALLNature/DigitallPower/commit/79e1735bf6fec291ca96ccc521785bb8c8e9ee06))
* **tsl:** extra auxiliary tester functions ([#136](https://github.com/DIGITALLNature/DigitallPower/issues/136)) ([a69fe7f](https://github.com/DIGITALLNature/DigitallPower/commit/a69fe7fd58d39b2457a930721af5ff81b1c42ad7))
* **tsl:** fix date attribute generation ([#133](https://github.com/DIGITALLNature/DigitallPower/issues/133)) ([f6d4fd5](https://github.com/DIGITALLNature/DigitallPower/commit/f6d4fd5111487ef5668f4da1dd85b21c85e4e031))
* **tsl:** fix issue with generics and random string inputs ([#123](https://github.com/DIGITALLNature/DigitallPower/issues/123)) ([a0d9046](https://github.com/DIGITALLNature/DigitallPower/commit/a0d904699550681b3f50cd9c51bbea58c5ca414b))
* **tsl:** form template missed subgrid and creates controls for all attributes even not in form ([#113](https://github.com/DIGITALLNature/DigitallPower/issues/113)) ([edd1fd9](https://github.com/DIGITALLNature/DigitallPower/commit/edd1fd924d3f4a2a33cefac8406b384b621b980b))
* **tsl:** further improvements to TypeScript models ([#108](https://github.com/DIGITALLNature/DigitallPower/issues/108)) ([ff898ac](https://github.com/DIGITALLNature/DigitallPower/commit/ff898ac320eefed2cf137d6183f5d5aa2e3c49c9))
* **tsl:** header bpf field mapping logic ([#115](https://github.com/DIGITALLNature/DigitallPower/issues/115)) ([293c36f](https://github.com/DIGITALLNature/DigitallPower/commit/293c36f94af5997ddf83246852a60fea1ac5b918))
* **tsl:** improve dto generation ([060ccb2](https://github.com/DIGITALLNature/DigitallPower/commit/060ccb2dee511acb3ef3ad2369326efaaf799075))
* **tsl:** improve test helper class generation ([#126](https://github.com/DIGITALLNature/DigitallPower/issues/126)) ([0958642](https://github.com/DIGITALLNature/DigitallPower/commit/09586420115d32c1b19ef01ca8a737bec64016ed))
* **tsl:** include formid in the generated classes for frontend logic to use ([#124](https://github.com/DIGITALLNature/DigitallPower/issues/124)) ([fe79624](https://github.com/DIGITALLNature/DigitallPower/commit/fe796246327930aea0f0b58dc6079e8918c15a30))
* **tsl:** rewrite form control loop from form xml ([#117](https://github.com/DIGITALLNature/DigitallPower/issues/117)) ([1489dee](https://github.com/DIGITALLNature/DigitallPower/commit/1489dee2d694b80d24107d376987144d556c154c))
* **tsl:** skip tabs and sections for quick view forms ([#129](https://github.com/DIGITALLNature/DigitallPower/issues/129)) ([a375433](https://github.com/DIGITALLNature/DigitallPower/commit/a375433ef26515c197e04001bce283d3702bab9e))
* **tsl:** small correction for empty section names and customer type attributess mapping ([#127](https://github.com/DIGITALLNature/DigitallPower/issues/127)) ([741b5b5](https://github.com/DIGITALLNature/DigitallPower/commit/741b5b592eed2aa90b795880bb73413383d67c91))
* **tsl:** small corrections on ts generation ([#116](https://github.com/DIGITALLNature/DigitallPower/issues/116)) ([7f19ccb](https://github.com/DIGITALLNature/DigitallPower/commit/7f19ccb7e92d381603ccd04a37b3c1ea6a4394a4))
* **tsl:** small improve in test mock tab handler ([#159](https://github.com/DIGITALLNature/DigitallPower/issues/159)) ([4de78e8](https://github.com/DIGITALLNature/DigitallPower/commit/4de78e836f8dd073a40d866f1542943453ebbe4f))
* **tsl:** support base language option for enums ([#109](https://github.com/DIGITALLNature/DigitallPower/issues/109)) ([1477cba](https://github.com/DIGITALLNature/DigitallPower/commit/1477cba16b316d573658722c8d3af0439299cf83))
* **tsl:** ts generation of ui.quickviews in form context ([#122](https://github.com/DIGITALLNature/DigitallPower/issues/122)) ([1e29ebf](https://github.com/DIGITALLNature/DigitallPower/commit/1e29ebf047dafee438bae75a890bb0757f92b4ab))


### Performance Improvements

* **push:** load solution webresources once and skip redundant AddSolutionComponent calls ([0cb8373](https://github.com/DIGITALLNature/DigitallPower/commit/0cb8373c251a618b87709769363fb9c6ce3500a8))

# [2.1.0-beta.60](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.59...v2.1.0-beta.60) (2026-07-01)


### Bug Fixes

* **codegeneration:** add null guards and use concrete return types in MetadataService ([3d1fa16](https://github.com/DIGITALLNature/DigitallPower/commit/3d1fa160c8c9798363b224ee4a1d5d45a783ef8e))
* **codegeneration:** ditch json polymorphic ([c84c1ec](https://github.com/DIGITALLNature/DigitallPower/commit/c84c1ecc4c63e11a71e1784eedc5b3553899da3d))
* **codegeneration:** polymorphic json config deserialization ([063163a](https://github.com/DIGITALLNature/DigitallPower/commit/063163a97376058ff96d1713860d0bc4a70ea0ad))
* **codegeneration:** resolve covariant array conversion warnings in MetadataService ([543545e](https://github.com/DIGITALLNature/DigitallPower/commit/543545edd5514e0a927eb9c60d37e99191bd32e3))
* resolve new Qodana findings in codegeneration and push modules ([3f20522](https://github.com/DIGITALLNature/DigitallPower/commit/3f2052290f82acf33436a4228f6944afd2850bda)), closes [#region](https://github.com/DIGITALLNature/DigitallPower/issues/region)


### Features

* **codegeneration:** redesign V2 config with scope/output separation ([09880c8](https://github.com/DIGITALLNature/DigitallPower/commit/09880c8940adab7cff1250f7c00913aba828d61c))

# [2.1.0-beta.59](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.58...v2.1.0-beta.59) (2026-06-29)


### Features

* **tsl:** small improve in test mock tab handler ([#159](https://github.com/DIGITALLNature/DigitallPower/issues/159)) ([4de78e8](https://github.com/DIGITALLNature/DigitallPower/commit/4de78e836f8dd073a40d866f1542943453ebbe4f))

# [2.1.0-beta.58](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.57...v2.1.0-beta.58) (2026-06-29)


### Performance Improvements

* **push:** load solution webresources once and skip redundant AddSolutionComponent calls ([0cb8373](https://github.com/DIGITALLNature/DigitallPower/commit/0cb8373c251a618b87709769363fb9c6ce3500a8))

# [2.1.0-beta.57](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.56...v2.1.0-beta.57) (2026-06-26)


### Features

* **profile:** add non-interactive auth mode for coding agents ([#157](https://github.com/DIGITALLNature/DigitallPower/issues/157)) ([f92e336](https://github.com/DIGITALLNature/DigitallPower/commit/f92e336b87a1400630eabd52094b9d4ddcfab684))

# [2.1.0-beta.56](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.55...v2.1.0-beta.56) (2026-06-26)


### Bug Fixes

* **push:** add existing webresources to solution on update and up2date ([829b9be](https://github.com/DIGITALLNature/DigitallPower/commit/829b9be0a1bbcccd010deed5e13a378bff862abe))


### Features

* add dotnet-suggest tab completion support ([0ebcf44](https://github.com/DIGITALLNature/DigitallPower/commit/0ebcf4434fe78c6d6f688d797074502ec0849cca))
* **complete:** add dgtp complete setup and install-shell commands ([0b58e2b](https://github.com/DIGITALLNature/DigitallPower/commit/0b58e2b0c9079dc3263c7dfa840df23290a8fe5e))
* **completion:** add dynamic profile name completions ([7841d58](https://github.com/DIGITALLNature/DigitallPower/commit/7841d586524d4641a44ccac2f94adb4f8cea2cf9))

# [2.1.0-beta.55](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.54...v2.1.0-beta.55) (2026-06-11)


### Bug Fixes

* **push:** address review feedback - migrate Custom APIs for PowerPlugins, use FirstOrDefault ([e4e55b7](https://github.com/DIGITALLNature/DigitallPower/commit/e4e55b7ee0a2e570e1b6c59034c99337997cdd3f))
* **push:** resolve analyzer warnings in AssemblyProcessor and migration tests ([2d12095](https://github.com/DIGITALLNature/DigitallPower/commit/2d12095a1d6eb1a8b1b71b8c7708415807a0b96a))


### Features

* **push:** migrate plugin steps and Custom API references on assembly version upgrade ([93a717f](https://github.com/DIGITALLNature/DigitallPower/commit/93a717f2e5c943469ff0e1b1920ca3190ac4464a)), closes [#91](https://github.com/DIGITALLNature/DigitallPower/issues/91)

# [2.1.0-beta.54](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.53...v2.1.0-beta.54) (2026-06-11)


### Bug Fixes

* **push:** rename managed identity assembly linker method ([d774569](https://github.com/DIGITALLNature/DigitallPower/commit/d7745695fc89d36fc1e3edde9c5c63781791b86c))


### Features

* **push:** evaluate ManagedIdentityRegistrationAttribute for identity linking ([68de0b2](https://github.com/DIGITALLNature/DigitallPower/commit/68de0b2e9c17792a195d97b54f0a82f13dc22d1c))

# [2.1.0-beta.53](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.52...v2.1.0-beta.53) (2026-06-11)


### Features

* **push:** evaluate CustomDataProviderRegistrationAttribute for step generation ([6c169b0](https://github.com/DIGITALLNature/DigitallPower/commit/6c169b0cc0c2e349734300fe50fd70d80fc2c561))

# [2.1.0-beta.52](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.51...v2.1.0-beta.52) (2026-06-11)


### Bug Fixes

* **tests:** adapt to regenerated Workflow.Options.Category naming ([1baa966](https://github.com/DIGITALLNature/DigitallPower/commit/1baa9664ab1c481892f9658f5cf7244bc17d28a4))


### Features

* **common:** add RoutingRuleItem partial extension for msdyn_routeto and assignobjectidtype ([9779b32](https://github.com/DIGITALLNature/DigitallPower/commit/9779b326b43b99f6c9bf90799316d45a57f875cd))

# [2.1.0-beta.51](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.50...v2.1.0-beta.51) (2026-06-10)


### Bug Fixes

* replace Thread.Sleep with Task.Delay in push-adjacent modules ([bd9ab75](https://github.com/DIGITALLNature/DigitallPower/commit/bd9ab756fc84db8958b963e9d111dcbbb5014599)), closes [#78](https://github.com/DIGITALLNature/DigitallPower/issues/78)


### Features

* **codegeneration:** remove brownout warnings for Actions, AdditionalSdkMessages and CustomAPIs ([0c58601](https://github.com/DIGITALLNature/DigitallPower/commit/0c58601948d6ebc34907de14103ff6400121e4b6)), closes [#89](https://github.com/DIGITALLNature/DigitallPower/issues/89)

# [2.1.0-beta.50](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.49...v2.1.0-beta.50) (2026-06-10)


### Bug Fixes

* **push:** change ExecutionOrder default from 1 to 100 ([5e580a4](https://github.com/DIGITALLNature/DigitallPower/commit/5e580a4b308dd515b68fc34d7d4bdd2c94ae8eab))
* sign test assemblies and build/test exclusively in Release ([3236382](https://github.com/DIGITALLNature/DigitallPower/commit/3236382cafb96d6efc01a6a5f2dc5cd55dca62b5)), closes [#if](https://github.com/DIGITALLNature/DigitallPower/issues/if)

# [2.1.0-beta.49](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.48...v2.1.0-beta.49) (2026-06-10)


### Bug Fixes

* extend known namespaces list ([2cd1174](https://github.com/DIGITALLNature/DigitallPower/commit/2cd1174481284222ae2ad8280ccf12cf6f0ca25b))

# [2.1.0-beta.48](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.47...v2.1.0-beta.48) (2026-06-08)


### Features

* **tsl:** correct xrm-type class in template ([#143](https://github.com/DIGITALLNature/DigitallPower/issues/143)) ([149df5a](https://github.com/DIGITALLNature/DigitallPower/commit/149df5adaa4744aeec3fdac14e3c9504813a2fb0))

# [2.1.0-beta.47](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.46...v2.1.0-beta.47) (2026-06-02)


### Features

* **tsl:** add small extra corrections to the unit tests utils ([#141](https://github.com/DIGITALLNature/DigitallPower/issues/141)) ([7734377](https://github.com/DIGITALLNature/DigitallPower/commit/7734377f005f3ad6aab93f88fcdc57dcec85e477))

# [2.1.0-beta.46](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.45...v2.1.0-beta.46) (2026-05-29)


### Features

* **telemetry:** expose beta version in activity source ([c055f0e](https://github.com/DIGITALLNature/DigitallPower/commit/c055f0e6ba3a9ecc423ecd9a4dc6bec868b47fe6))

# [2.1.0-beta.45](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.44...v2.1.0-beta.45) (2026-05-28)


### Bug Fixes

* **codegeneration:** eliminate empty lines in Entity.dotnet.liquid for-loops ([27f4008](https://github.com/DIGITALLNature/DigitallPower/commit/27f40084e1d121e1e6da739919ee302a3dbbd9dd))
* **codegeneration:** normalize all dotnet Liquid templates to 4 spaces ([3487078](https://github.com/DIGITALLNature/DigitallPower/commit/34870781b88671802f45821399315c940e257531))
* **codegeneration:** propagate worker failure to exit code ([2a15cb3](https://github.com/DIGITALLNature/DigitallPower/commit/2a15cb3fdfdd71d56f39a61a284a2b597e0c5a3d))
* complete TUnit assertion API migration ([b4ebb8e](https://github.com/DIGITALLNature/DigitallPower/commit/b4ebb8e93a27e5ccfffa3f96bc320f94acb9bf99))
* correct TUnit Count() API usage - use IsEqualTo not EqualTo ([e318a0f](https://github.com/DIGITALLNature/DigitallPower/commit/e318a0f700fd9f7dae07d1ba1e2c31412c6ddd30))
* migrate TUnit assertions from HasCount() to Count() ([157dd99](https://github.com/DIGITALLNature/DigitallPower/commit/157dd99784e9d16f6d96b9d9cd80456e220519b0))
* plugin package upgrade ([1454086](https://github.com/DIGITALLNature/DigitallPower/commit/1454086514a087f91da16a98168a6b80955b081d))
* resolve high-priority code analysis warnings ([f71be1f](https://github.com/DIGITALLNature/DigitallPower/commit/f71be1f67a65f88363ea0e886bdda9e6bded4380)), closes [hi#priority](https://github.com/hi/issues/priority)
* resolve Qodana static analysis findings ([58b5a26](https://github.com/DIGITALLNature/DigitallPower/commit/58b5a26efa9f6749d7f9a5c202bc967e3dfc770c))
* **telemetry:** align env var name to DGT_TELEMETRY_CONNECTION_STRING ([12f8521](https://github.com/DIGITALLNature/DigitallPower/commit/12f852188201239d255b6cfef64ce20b4e1834e6))
* **test:** update Digitall.Dataverse.Testing to beta.6 and fix BulkDelete test ([007f20f](https://github.com/DIGITALLNature/DigitallPower/commit/007f20f41ebb05883c4017a5e18db5d282bf286d))
* update Digitall.Dataverse.Testing to beta.8 and fix telemetry env var typo ([a1906fd](https://github.com/DIGITALLNature/DigitallPower/commit/a1906fd1e2a089907b09e85fdc0deddd0610eb69))


### Features

* add detailed option to create workflowstate config ([aca08f2](https://github.com/DIGITALLNature/DigitallPower/commit/aca08f2c963c1404e5cd31d1c637b9aa617fe772))
* add OpenTelemetry usage telemetry with opt-out ([4676f12](https://github.com/DIGITALLNature/DigitallPower/commit/4676f1228163d6ee6250f4bd81f03ae113ecd8fc))
* assembly with major version change should replace older version and allow purge of outdated ([6ded506](https://github.com/DIGITALLNature/DigitallPower/commit/6ded5068c1a63b229425c305f0879ce50c8ddd55))
* **telemetry:** embed connection string at build time via AssemblyMetadata ([8003a80](https://github.com/DIGITALLNature/DigitallPower/commit/8003a808e5af233ab2058c8ad05935f03ea90c1c))

# [2.1.0-beta.44](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.43...v2.1.0-beta.44) (2026-05-25)


### Features

* **tsl:** add additional extra mock function in unit test builder ([#138](https://github.com/DIGITALLNature/DigitallPower/issues/138)) ([e0daf8c](https://github.com/DIGITALLNature/DigitallPower/commit/e0daf8cacc0c91fc476d7190bfd18a70076ecd15))

# [2.1.0-beta.43](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.42...v2.1.0-beta.43) (2026-05-20)


### Features

* **tsl:** correct issue with the generation of control form when name not attribute ([#137](https://github.com/DIGITALLNature/DigitallPower/issues/137)) ([8b6cde7](https://github.com/DIGITALLNature/DigitallPower/commit/8b6cde766f1fcab261221b40218c7b77788ebcef))

# [2.1.0-beta.42](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.41...v2.1.0-beta.42) (2026-05-19)


### Features

* **tsl:** extra auxiliary tester functions ([#136](https://github.com/DIGITALLNature/DigitallPower/issues/136)) ([a69fe7f](https://github.com/DIGITALLNature/DigitallPower/commit/a69fe7fd58d39b2457a930721af5ff81b1c42ad7))

# [2.1.0-beta.41](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.40...v2.1.0-beta.41) (2026-05-19)


### Features

* **tsl:** add extra test mock util functions ([#135](https://github.com/DIGITALLNature/DigitallPower/issues/135)) ([d1be7d0](https://github.com/DIGITALLNature/DigitallPower/commit/d1be7d0625646c312bbe3a7f0ac829c99daa13a3))

# [2.1.0-beta.40](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.39...v2.1.0-beta.40) (2026-04-17)


### Features

* **tsl:** add type as branding property to distinguish them ([#134](https://github.com/DIGITALLNature/DigitallPower/issues/134)) ([108c468](https://github.com/DIGITALLNature/DigitallPower/commit/108c468d2f7fae622b4f5946239bd83b4d3bc85e))

# [2.1.0-beta.39](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.38...v2.1.0-beta.39) (2026-03-17)


### Features

* **tsl:** fix date attribute generation ([#133](https://github.com/DIGITALLNature/DigitallPower/issues/133)) ([f6d4fd5](https://github.com/DIGITALLNature/DigitallPower/commit/f6d4fd5111487ef5668f4da1dd85b21c85e4e031))

# [2.1.0-beta.38](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.37...v2.1.0-beta.38) (2026-02-19)


### Features

* **tsl:** added extra handling for lookup with multiple targets polymorphic ones ([#130](https://github.com/DIGITALLNature/DigitallPower/issues/130)) ([43634ce](https://github.com/DIGITALLNature/DigitallPower/commit/43634cecbbd51da5fefaea38a0d8a985de1f9311))

# [2.1.0-beta.37](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.36...v2.1.0-beta.37) (2026-02-18)


### Features

* **tsl:** skip tabs and sections for quick view forms ([#129](https://github.com/DIGITALLNature/DigitallPower/issues/129)) ([a375433](https://github.com/DIGITALLNature/DigitallPower/commit/a375433ef26515c197e04001bce283d3702bab9e))

# [2.1.0-beta.36](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.35...v2.1.0-beta.36) (2026-02-17)


### Features

* **tsl:** small correction for empty section names and customer type attributess mapping ([#127](https://github.com/DIGITALLNature/DigitallPower/issues/127)) ([741b5b5](https://github.com/DIGITALLNature/DigitallPower/commit/741b5b592eed2aa90b795880bb73413383d67c91))

# [2.1.0-beta.35](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.34...v2.1.0-beta.35) (2026-02-16)


### Bug Fixes

* **tsl:** proper default value when Attributemetadata is nowhere to be found ([7af645e](https://github.com/DIGITALLNature/DigitallPower/commit/7af645e82ef6f070fce6c09c02d22050e5dffded))

# [2.1.0-beta.34](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.33...v2.1.0-beta.34) (2026-02-16)


### Features

* **tsl:** create initial test helper classes ([#125](https://github.com/DIGITALLNature/DigitallPower/issues/125)) ([884f934](https://github.com/DIGITALLNature/DigitallPower/commit/884f934d6ddc4199304d7ba9f7cbf03bd2079336))
* **tsl:** improve test helper class generation ([#126](https://github.com/DIGITALLNature/DigitallPower/issues/126)) ([0958642](https://github.com/DIGITALLNature/DigitallPower/commit/09586420115d32c1b19ef01ca8a737bec64016ed))

# [2.1.0-beta.33](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.32...v2.1.0-beta.33) (2026-01-30)


### Features

* **tsl:** include formid in the generated classes for frontend logic to use ([#124](https://github.com/DIGITALLNature/DigitallPower/issues/124)) ([fe79624](https://github.com/DIGITALLNature/DigitallPower/commit/fe796246327930aea0f0b58dc6079e8918c15a30))

# [2.1.0-beta.32](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.31...v2.1.0-beta.32) (2026-01-27)


### Features

* **tsl:** fix issue with generics and random string inputs ([#123](https://github.com/DIGITALLNature/DigitallPower/issues/123)) ([a0d9046](https://github.com/DIGITALLNature/DigitallPower/commit/a0d904699550681b3f50cd9c51bbea58c5ca414b))

# [2.1.0-beta.31](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.30...v2.1.0-beta.31) (2026-01-26)


### Features

* **tsl:** ts generation of ui.quickviews in form context ([#122](https://github.com/DIGITALLNature/DigitallPower/issues/122)) ([1e29ebf](https://github.com/DIGITALLNature/DigitallPower/commit/1e29ebf047dafee438bae75a890bb0757f92b4ab))

# [2.1.0-beta.30](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.29...v2.1.0-beta.30) (2026-01-23)


### Features

* **tsl:** add hierarchical namespaces to the form and entities ([#121](https://github.com/DIGITALLNature/DigitallPower/issues/121)) ([4ce3692](https://github.com/DIGITALLNature/DigitallPower/commit/4ce3692c59cfb4ef27600b2e8105113f9e2f6526))

# [2.1.0-beta.29](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.28...v2.1.0-beta.29) (2026-01-23)


### Features

* **tsl:** adapt logic so ts files have a folder structure ([#120](https://github.com/DIGITALLNature/DigitallPower/issues/120)) ([d3a70f6](https://github.com/DIGITALLNature/DigitallPower/commit/d3a70f6f8ff48af31b201bc26cbe764ed4c8b3b2))

# [2.1.0-beta.28](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.27...v2.1.0-beta.28) (2026-01-22)


### Features

* **tsl:** extra adaptations for ts custom api generation ([#119](https://github.com/DIGITALLNature/DigitallPower/issues/119)) ([79e1735](https://github.com/DIGITALLNature/DigitallPower/commit/79e1735bf6fec291ca96ccc521785bb8c8e9ee06))

# [2.1.0-beta.27](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.26...v2.1.0-beta.27) (2026-01-21)


### Features

* **tsl:** add quick view mapping sort header process controls ([#118](https://github.com/DIGITALLNature/DigitallPower/issues/118)) ([28e06d4](https://github.com/DIGITALLNature/DigitallPower/commit/28e06d4688e080f237bcf039c148f22359f9864f))

# [2.1.0-beta.26](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.25...v2.1.0-beta.26) (2026-01-21)


### Bug Fixes

* **tsl:** resolving of Attribute via logicalname in filters now returns generic one when not found ([809568a](https://github.com/DIGITALLNature/DigitallPower/commit/809568a3d65d62e9f7d37427a991ed26575a407e))

# [2.1.0-beta.25](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.24...v2.1.0-beta.25) (2026-01-21)


### Bug Fixes

* **tsl:** graceful fallback when no localized label found ([9de4a6a](https://github.com/DIGITALLNature/DigitallPower/commit/9de4a6a0e623a44ca402da08f8a2a485cad97e74))

# [2.1.0-beta.24](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.23...v2.1.0-beta.24) (2026-01-21)


### Features

* **tsl:** rewrite form control loop from form xml ([#117](https://github.com/DIGITALLNature/DigitallPower/issues/117)) ([1489dee](https://github.com/DIGITALLNature/DigitallPower/commit/1489dee2d694b80d24107d376987144d556c154c))

# [2.1.0-beta.23](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.22...v2.1.0-beta.23) (2026-01-20)


### Features

* **tsl:** small corrections on ts generation ([#116](https://github.com/DIGITALLNature/DigitallPower/issues/116)) ([7f19ccb](https://github.com/DIGITALLNature/DigitallPower/commit/7f19ccb7e92d381603ccd04a37b3c1ea6a4394a4))

# [2.1.0-beta.22](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.21...v2.1.0-beta.22) (2026-01-14)


### Features

* **tsl:** header bpf field mapping logic ([#115](https://github.com/DIGITALLNature/DigitallPower/issues/115)) ([293c36f](https://github.com/DIGITALLNature/DigitallPower/commit/293c36f94af5997ddf83246852a60fea1ac5b918))

# [2.1.0-beta.21](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.20...v2.1.0-beta.21) (2026-01-09)


### Bug Fixes

* **tsl:** use unique for solution form creation ([#114](https://github.com/DIGITALLNature/DigitallPower/issues/114)) ([f78579c](https://github.com/DIGITALLNature/DigitallPower/commit/f78579c02440a880361872858570ee8756207885))

# [2.1.0-beta.20](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.19...v2.1.0-beta.20) (2025-12-19)


### Bug Fixes

* **tsl:** add delegate overload to form control collection ([24a8bbc](https://github.com/DIGITALLNature/DigitallPower/commit/24a8bbc254e7a3e0610d828f5c67958ebdae289f))

# [2.1.0-beta.19](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.18...v2.1.0-beta.19) (2025-12-19)


### Bug Fixes

* **tsl:** correctly skip form generation for bpf entities ([2d3fa95](https://github.com/DIGITALLNature/DigitallPower/commit/2d3fa95f001ebb2a4a5f9c6a7a6735599cbe422e))

# [2.1.0-beta.18](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.17...v2.1.0-beta.18) (2025-12-16)


### Features

* **tsl:** form template missed subgrid and creates controls for all attributes even not in form ([#113](https://github.com/DIGITALLNature/DigitallPower/issues/113)) ([edd1fd9](https://github.com/DIGITALLNature/DigitallPower/commit/edd1fd924d3f4a2a33cefac8406b384b621b980b))

# [2.1.0-beta.17](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.16...v2.1.0-beta.17) (2025-12-10)


### Features

* **tsl:** support base language option for enums ([#109](https://github.com/DIGITALLNature/DigitallPower/issues/109)) ([1477cba](https://github.com/DIGITALLNature/DigitallPower/commit/1477cba16b316d573658722c8d3af0439299cf83))

# [2.1.0-beta.16](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.15...v2.1.0-beta.16) (2025-11-27)


### Bug Fixes

* **tsl:** fix broken Entity enum ([ce136dc](https://github.com/DIGITALLNature/DigitallPower/commit/ce136dc432c8689403f7c1910149828bee211da6))

# [2.1.0-beta.15](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.14...v2.1.0-beta.15) (2025-11-26)


### Bug Fixes

* **tsl:** fix inlining of entity logical name constant ([16a8a01](https://github.com/DIGITALLNature/DigitallPower/commit/16a8a01f838c3db5d22b106680be50901661b811))

# [2.1.0-beta.14](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.13...v2.1.0-beta.14) (2025-11-24)


### Features

* **tsl:** further improvements to TypeScript models ([#108](https://github.com/DIGITALLNature/DigitallPower/issues/108)) ([ff898ac](https://github.com/DIGITALLNature/DigitallPower/commit/ff898ac320eefed2cf137d6183f5d5aa2e3c49c9))

# [2.1.0-beta.13](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.12...v2.1.0-beta.13) (2025-11-18)


### Features

* **tsl:** improve dto generation ([060ccb2](https://github.com/DIGITALLNature/DigitallPower/commit/060ccb2dee511acb3ef3ad2369326efaaf799075))

# [2.1.0-beta.12](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.11...v2.1.0-beta.12) (2025-11-18)


### Bug Fixes

* dto properties formatting ([a664ded](https://github.com/DIGITALLNature/DigitallPower/commit/a664ded55a92cf2d9cfbd873b0064a2264a3d163))

# [2.1.0-beta.11](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.10...v2.1.0-beta.11) (2025-11-18)


### Features

* **tsl:** enhance generated types ([#107](https://github.com/DIGITALLNature/DigitallPower/issues/107)) ([8cdd380](https://github.com/DIGITALLNature/DigitallPower/commit/8cdd38093f8181f0b8466cd74640c5b06b26c2ce))

# [2.1.0-beta.10](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.9...v2.1.0-beta.10) (2025-11-17)


### Bug Fixes

* incompatible update of Microsoft.AspNetCore.DataProtection ([4c75604](https://github.com/DIGITALLNature/DigitallPower/commit/4c75604c6275951ea809a894408974f1e1cc5462))

# [2.1.0-beta.9](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.8...v2.1.0-beta.9) (2025-11-17)


### Features

* **tsl:** add Typescript Light DTO generation for Entites ([3c66950](https://github.com/DIGITALLNature/DigitallPower/commit/3c6695093282dd5f591bfa23df978b523a1efa16))

# [2.1.0-beta.8](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.7...v2.1.0-beta.8) (2025-11-04)


### Features

* lightweight model generation for TypeScript ([992c8c7](https://github.com/DIGITALLNature/DigitallPower/commit/992c8c758bf914667f9f27e630640771f8ab2edf))

# [2.1.0-beta.7](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.6...v2.1.0-beta.7) (2025-08-27)


### Bug Fixes

* escape markup for component name ([#105](https://github.com/DIGITALLNature/DigitallPower/issues/105)) ([4d9bf34](https://github.com/DIGITALLNature/DigitallPower/commit/4d9bf341ada770a1e031a740327468f48c97a340))

# [2.1.0-beta.6](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.5...v2.1.0-beta.6) (2025-07-02)


### Features

* add msal example ([fb594d7](https://github.com/DIGITALLNature/DigitallPower/commit/fb594d7140b684fe12668251309e4e350c4b1645))

# [2.1.0-beta.5](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.4...v2.1.0-beta.5) (2025-07-02)


### Features

* add semi global profile parameter to run commands against specific profile ([#102](https://github.com/DIGITALLNature/DigitallPower/issues/102)) ([e368247](https://github.com/DIGITALLNature/DigitallPower/commit/e3682472e5878ca7047252ef26e8740c6d92b77a)), closes [#103](https://github.com/DIGITALLNature/DigitallPower/issues/103)

# [2.1.0-beta.4](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.3...v2.1.0-beta.4) (2025-05-26)


### Bug Fixes

* link plugin to custom api ([#100](https://github.com/DIGITALLNature/DigitallPower/issues/100)) ([56a1d2a](https://github.com/DIGITALLNature/DigitallPower/commit/56a1d2abb007999af0305a964a0119b89bcb7e4b))

# [2.1.0-beta.3](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.2...v2.1.0-beta.3) (2025-03-20)


### Features

* lighter typescript model generation ([#79](https://github.com/DIGITALLNature/DigitallPower/issues/79)) ([978678b](https://github.com/DIGITALLNature/DigitallPower/commit/978678bea4b2babfdbaef1137f2aae40d96d0c07))

# [2.1.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.1...v2.1.0-beta.2) (2025-02-27)


### Bug Fixes

* update dotnet version mentioned ([af0dd1c](https://github.com/DIGITALLNature/DigitallPower/commit/af0dd1c8c9e8b46a7a180b16890a565424fdd3c4))
* use home account identifier for msal token ([b05ac7c](https://github.com/DIGITALLNature/DigitallPower/commit/b05ac7c7d9c2fab555dcae1205c9df213dae8479))

# [2.1.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v2.1.0-beta.1...v2.1.0-beta.2) (2025-02-26)


### Bug Fixes

* use home account identifier for msal token ([b05ac7c](https://github.com/DIGITALLNature/DigitallPower/commit/b05ac7c7d9c2fab555dcae1205c9df213dae8479))

# [2.1.0-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.1-beta.2...v2.1.0-beta.1) (2025-01-08)


### Bug Fixes

* add necessary test data ([3e4b373](https://github.com/DIGITALLNature/DigitallPower/commit/3e4b37396f08ac335ad5f7a479b326afe5aa7504))


### Features

* treat macro as workflow with normal name ([6e6a8db](https://github.com/DIGITALLNature/DigitallPower/commit/6e6a8db8dc1952747f5092ab479f303050412876))

## [2.0.1-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.1-beta.1...v2.0.1-beta.2) (2024-06-20)


### Bug Fixes

* problem with adding a dependent plugin to a solution ([5f8696e](https://github.com/DIGITALLNature/DigitallPower/commit/5f8696e86d87d460052b79bef0dedd960c6d4247))

## [2.0.1-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0...v2.0.1-beta.1) (2024-05-28)


### Bug Fixes

* **maintenance workflowstate:** assign workflows before enabling then  Mitigates issues with licensing when old owner is no longer licensed ([d1da4c5](https://github.com/DIGITALLNature/DigitallPower/commit/d1da4c5bd33d5b8a0ca8569de3f6a2b2b09b6587))

# [2.0.0-beta.9](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0-beta.8...v2.0.0-beta.9) (2024-05-28)


### Bug Fixes

* **maintenance workflowstate:** assign workflows before enabling then  Mitigates issues with licensing when old owner is no longer licensed ([d1da4c5](https://github.com/DIGITALLNature/DigitallPower/commit/d1da4c5bd33d5b8a0ca8569de3f6a2b2b09b6587))

# [2.0.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.12.1...v2.0.0) (2024-05-24)


### Bug Fixes

* **maintenance:** correct handling if both carrier-solutions are present ([bbc7b7d](https://github.com/DIGITALLNature/DigitallPower/commit/bbc7b7d3b7b38750d5f214f22238b896d1c896bf))
* **push-package:** dispose to not block file ([#73](https://github.com/DIGITALLNature/DigitallPower/issues/73)) ([5ed2993](https://github.com/DIGITALLNature/DigitallPower/commit/5ed2993da4823e6aa284f2dbce854fd92d940c5b))


### Features

* **analyze:** add redundantpatches check ([#80](https://github.com/DIGITALLNature/DigitallPower/issues/80)) ([a13d169](https://github.com/DIGITALLNature/DigitallPower/commit/a13d169206c8ab1a883541a4926cc48ce69e77e4))
* change namespace to Digitall.APower [#66](https://github.com/DIGITALLNature/DigitallPower/issues/66) ([#68](https://github.com/DIGITALLNature/DigitallPower/issues/68)) ([6e33c4e](https://github.com/DIGITALLNature/DigitallPower/commit/6e33c4e929fc8c5d721ca6c6d721908897fa1359))
* **maintenance:** add FilterPowerFxPluginSteps ([c32fe26](https://github.com/DIGITALLNature/DigitallPower/commit/c32fe269ebc913fd3f74103656439842fb659cdb))
* **maintenance:** check for dgt carrier first ([fb5135c](https://github.com/DIGITALLNature/DigitallPower/commit/fb5135cc815c9d502ed006ad457e16173a3db8d9))
* **Maintenance:** option to remove redundant components in a solution compared with multiple sources solutions ([cecfac4](https://github.com/DIGITALLNature/DigitallPower/commit/cecfac46ccca2439e9a0bb0844c5fb07efce18dd))
* **profile:** add option to do token based authentication ([c1bdd87](https://github.com/DIGITALLNature/DigitallPower/commit/c1bdd87185453e0bed4c3e3cc143856e6d639332))
* **push:** allow mapping to push webresources ([4d6666d](https://github.com/DIGITALLNature/DigitallPower/commit/4d6666d258948de0711200f62490638af3f8405a))
* waiting time for dataverse operations is now configurable ([5228d7e](https://github.com/DIGITALLNature/DigitallPower/commit/5228d7e244eb18ae0a9cf749f99aa3c248093bd8))
* **workflowstate:** include other workflow categories and introduce config generator ([50260a1](https://github.com/DIGITALLNature/DigitallPower/commit/50260a187ddcc7463d6ffaa1b4293928f8aca8b3))


### BREAKING CHANGES

* **push:** no longer tries to delete the webresource if adding to solution fails

Co-Authored-By: Micha Oberstein <Moberstein@users.noreply.github.com>

# [2.0.0-beta.8](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0-beta.7...v2.0.0-beta.8) (2024-03-28)


### Features

* **analyze:** add redundantpatches check ([#80](https://github.com/DIGITALLNature/DigitallPower/issues/80)) ([a13d169](https://github.com/DIGITALLNature/DigitallPower/commit/a13d169206c8ab1a883541a4926cc48ce69e77e4))

# [2.0.0-beta.7](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0-beta.6...v2.0.0-beta.7) (2024-02-28)


### Features

* waiting time for dataverse operations is now configurable ([5228d7e](https://github.com/DIGITALLNature/DigitallPower/commit/5228d7e244eb18ae0a9cf749f99aa3c248093bd8))

# [2.0.0-beta.6](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0-beta.5...v2.0.0-beta.6) (2024-02-27)


### Features

* **workflowstate:** include other workflow categories and introduce config generator ([50260a1](https://github.com/DIGITALLNature/DigitallPower/commit/50260a187ddcc7463d6ffaa1b4293928f8aca8b3))

# [2.0.0-beta.5](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0-beta.4...v2.0.0-beta.5) (2024-01-03)


### Bug Fixes

* **push-package:** dispose to not block file ([#73](https://github.com/DIGITALLNature/DigitallPower/issues/73)) ([5ed2993](https://github.com/DIGITALLNature/DigitallPower/commit/5ed2993da4823e6aa284f2dbce854fd92d940c5b))

# [2.0.0-beta.4](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0-beta.3...v2.0.0-beta.4) (2024-01-03)


### Features

* **profile:** add option to do token based authentication ([c1bdd87](https://github.com/DIGITALLNature/DigitallPower/commit/c1bdd87185453e0bed4c3e3cc143856e6d639332))

# [2.0.0-beta.3](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0-beta.2...v2.0.0-beta.3) (2024-01-03)


### Bug Fixes

* **maintenance:** correct handling if both carrier-solutions are present ([bbc7b7d](https://github.com/DIGITALLNature/DigitallPower/commit/bbc7b7d3b7b38750d5f214f22238b896d1c896bf))

# [2.0.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v2.0.0-beta.1...v2.0.0-beta.2) (2024-01-02)


### Features

* **maintenance:** check for dgt carrier first ([fb5135c](https://github.com/DIGITALLNature/DigitallPower/commit/fb5135cc815c9d502ed006ad457e16173a3db8d9))

# [2.0.0-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.13.0-beta.3...v2.0.0-beta.1) (2024-01-02)


### Features

* **push:** allow mapping to push webresources ([4d6666d](https://github.com/DIGITALLNature/DigitallPower/commit/4d6666d258948de0711200f62490638af3f8405a))


### BREAKING CHANGES

* **push:** no longer tries to delete the webresource if adding to solution fails

Co-Authored-By: Micha Oberstein <Moberstein@users.noreply.github.com>

# [1.13.0-beta.3](https://github.com/DIGITALLNature/DigitallPower/compare/v1.13.0-beta.2...v1.13.0-beta.3) (2023-11-23)


### Features

* change namespace to Digitall.APower [#66](https://github.com/DIGITALLNature/DigitallPower/issues/66) ([#68](https://github.com/DIGITALLNature/DigitallPower/issues/68)) ([6e33c4e](https://github.com/DIGITALLNature/DigitallPower/commit/6e33c4e929fc8c5d721ca6c6d721908897fa1359))

# [1.13.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v1.13.0-beta.1...v1.13.0-beta.2) (2023-11-03)


### Features

* **maintenance:** add FilterPowerFxPluginSteps ([c32fe26](https://github.com/DIGITALLNature/DigitallPower/commit/c32fe269ebc913fd3f74103656439842fb659cdb))

# [1.13.0-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.12.1...v1.13.0-beta.1) (2023-09-08)


### Features

* **Maintenance:** option to remove redundant components in a solution compared with multiple sources solutions ([cecfac4](https://github.com/DIGITALLNature/DigitallPower/commit/cecfac46ccca2439e9a0bb0844c5fb07efce18dd))

## [1.12.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.12.0...v1.12.1) (2023-07-27)


### Bug Fixes

* **UpdateWorkflowState:** add EscapeMarkup in  Logging ([#61](https://github.com/DIGITALLNature/DigitallPower/issues/61)) ([299ca8e](https://github.com/DIGITALLNature/DigitallPower/commit/299ca8ed48e63319e7b52d51de564b4d00090685))

# [1.12.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.11.0...v1.12.0) (2023-07-10)


### Features

* **maintenane:** support DGT Solution concept in ExportCarrierInfo ([#60](https://github.com/DIGITALLNature/DigitallPower/issues/60)) ([98b7fd1](https://github.com/DIGITALLNature/DigitallPower/commit/98b7fd1cef28cd4560ae9f70408325a636fe4346))

# [1.11.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.10.1...v1.11.0) (2023-06-29)


### Bug Fixes

* **codegeneration:** generate odatattributes as well (again) ([#58](https://github.com/DIGITALLNature/DigitallPower/issues/58)) ([4b8653c](https://github.com/DIGITALLNature/DigitallPower/commit/4b8653c9bcc931c33ab610dede4cb9a436b61e40)), closes [#55](https://github.com/DIGITALLNature/DigitallPower/issues/55) [#55](https://github.com/DIGITALLNature/DigitallPower/issues/55)


### Features

* **push:** add option to add plugin packages to solution ([#57](https://github.com/DIGITALLNature/DigitallPower/issues/57)) ([076b680](https://github.com/DIGITALLNature/DigitallPower/commit/076b680411443141919d815005f00df2abf79c3e))

# [1.11.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v1.11.0-beta.1...v1.11.0-beta.2) (2023-06-29)


### Bug Fixes

* **codegeneration:** generate odatattributes as well (again) ([#58](https://github.com/DIGITALLNature/DigitallPower/issues/58)) ([037b8a6](https://github.com/DIGITALLNature/DigitallPower/commit/037b8a68098ad09f0e8e29f1f946a9f8017f2013)), closes [#55](https://github.com/DIGITALLNature/DigitallPower/issues/55) [#55](https://github.com/DIGITALLNature/DigitallPower/issues/55)

# [1.11.0-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.10.1...v1.11.0-beta.1) (2023-06-27)


### Features

* **push:** add option to add plugin packages to solution ([#57](https://github.com/DIGITALLNature/DigitallPower/issues/57)) ([c230c0e](https://github.com/DIGITALLNature/DigitallPower/commit/c230c0ea18e6a5fc59d8e91153ebfa59696e19b7))

## [1.10.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.10.0...v1.10.1) (2023-06-26)


### Bug Fixes

* **push:** replace backslash in webresources ([#55](https://github.com/DIGITALLNature/DigitallPower/issues/55)) ([fc5166c](https://github.com/DIGITALLNature/DigitallPower/commit/fc5166c9ce5957e27877e901579036b51f691464))

# [1.10.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.0...v1.10.0) (2023-06-23)


### Bug Fixes

* **codegeneration:** generate invalid odatattributes as well ([#51](https://github.com/DIGITALLNature/DigitallPower/issues/51)) ([6606a85](https://github.com/DIGITALLNature/DigitallPower/commit/6606a85b436e1ac7d9d7ed9c47a4ed6c6d9797c9))
* **general:**  correct local file versions ([116ab1d](https://github.com/DIGITALLNature/DigitallPower/commit/116ab1d1cec3cc25945c1e4bc6370ddddbeb15a2))
* **general:** correct --version cli output ([e3641b2](https://github.com/DIGITALLNature/DigitallPower/commit/e3641b276cd79c31f406f13b97f1df5a681ba5da))
* **general:** versioning of all dgtp artifacts ([97b4fe1](https://github.com/DIGITALLNature/DigitallPower/commit/97b4fe14883636d0fc9ef176bfe1d94af24c8766))


### Features

* **Push:** dependant assembly push with proper metadata inspection ([f78ef19](https://github.com/DIGITALLNature/DigitallPower/commit/f78ef1937917131a61238af831d25dac6f70dc75))

# [1.10.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v1.10.0-beta.1...v1.10.0-beta.2) (2023-06-23)


### Bug Fixes

* **codegeneration:** generate invalid odatattributes as well ([#51](https://github.com/DIGITALLNature/DigitallPower/issues/51)) ([8363f7d](https://github.com/DIGITALLNature/DigitallPower/commit/8363f7d559f832f1d1b5b3487356805e68560fa1))

# [1.10.0-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.1-beta.3...v1.10.0-beta.1) (2023-05-23)


### Features

* **Push:** dependant assembly push with proper metadata inspection ([4220072](https://github.com/DIGITALLNature/DigitallPower/commit/42200727eff53b3278cfebac3eeb88ffe52b06e4))

## [1.9.1-beta.3](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.1-beta.2...v1.9.1-beta.3) (2023-05-17)


### Bug Fixes

* **general:** versioning of all dgtp artifacts ([f51f609](https://github.com/DIGITALLNature/DigitallPower/commit/f51f609927113ebacfbef420a2ba37526880fd3b))

## [1.9.1-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.1-beta.1...v1.9.1-beta.2) (2023-05-17)


### Bug Fixes

* **general:**  correct local file versions ([751a840](https://github.com/DIGITALLNature/DigitallPower/commit/751a840b4ef84297f02e4de3c6e6d5f077dcde79))

## [1.9.1-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.0...v1.9.1-beta.1) (2023-05-17)


### Bug Fixes

* **general:** correct --version cli output ([331bf0f](https://github.com/DIGITALLNature/DigitallPower/commit/331bf0fca990a1d51f0966ae629886bb67cade2f))

# [1.9.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.8.0...v1.9.0) (2023-05-16)


### Bug Fixes

* **codegeneration:** incorrect parameters for CustomAPIs ([0518d5a](https://github.com/DIGITALLNature/DigitallPower/commit/0518d5a3679aeaed7a1c9713dba15ac45f4a4d10))
* **codegeneration:** sanitize parameternames in actions ([9920124](https://github.com/DIGITALLNature/DigitallPower/commit/992012488e5e933a279787eee08fcbe81e035476))
* **maintain): fix(maintainance:** repaired resilience at workflowstate ([d60e9da](https://github.com/DIGITALLNature/DigitallPower/commit/d60e9dac012d2a732cb228463a6b2ae110d62abc))


### Features

* **codegeneration:** changed attribute filter to IsValidODataAttribute ([5012ead](https://github.com/DIGITALLNature/DigitallPower/commit/5012ead4827e0af256e39ff0b4dcc08024008fa7))
* **push:** allow Publish after creation of an element ([662d787](https://github.com/DIGITALLNature/DigitallPower/commit/662d7871b6a2d3587260cf65bd4b4bdb24babd26))
* **push:** upload and sync of webressources ([#46](https://github.com/DIGITALLNature/DigitallPower/issues/46)) ([f59f90e](https://github.com/DIGITALLNature/DigitallPower/commit/f59f90e92b910ce6dd183feef7aafb9c301998fe))

# [1.9.0-beta.6](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.0-beta.5...v1.9.0-beta.6) (2023-05-16)


### Features

* **codegeneration:** changed attribute filter to IsValidODataAttribute ([5012ead](https://github.com/DIGITALLNature/DigitallPower/commit/5012ead4827e0af256e39ff0b4dcc08024008fa7))

# [1.9.0-beta.5](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.0-beta.4...v1.9.0-beta.5) (2023-05-16)


### Bug Fixes

* **codegeneration:** sanitize parameternames in actions ([9920124](https://github.com/DIGITALLNature/DigitallPower/commit/992012488e5e933a279787eee08fcbe81e035476))

# [1.9.0-beta.4](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.0-beta.3...v1.9.0-beta.4) (2023-05-05)


### Bug Fixes

* **codegeneration:** incorrect parameters for CustomAPIs ([0518d5a](https://github.com/DIGITALLNature/DigitallPower/commit/0518d5a3679aeaed7a1c9713dba15ac45f4a4d10))

# [1.9.0-beta.3](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.0-beta.2...v1.9.0-beta.3) (2023-04-14)


### Bug Fixes

* **maintain): fix(maintainance:** repaired resilience at workflowstate ([d60e9da](https://github.com/DIGITALLNature/DigitallPower/commit/d60e9dac012d2a732cb228463a6b2ae110d62abc))

# [1.9.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v1.9.0-beta.1...v1.9.0-beta.2) (2023-04-14)


### Features

* **push:** allow Publish after creation of an element ([662d787](https://github.com/DIGITALLNature/DigitallPower/commit/662d7871b6a2d3587260cf65bd4b4bdb24babd26))

# [1.9.0-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.8.0...v1.9.0-beta.1) (2023-04-06)


### Features

* **push:** upload and sync of webressources ([#46](https://github.com/DIGITALLNature/DigitallPower/issues/46)) ([f59f90e](https://github.com/DIGITALLNature/DigitallPower/commit/f59f90e92b910ce6dd183feef7aafb9c301998fe))

# [1.8.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.7.1...v1.8.0) (2023-04-06)


### Features

* **dgt.power:** check if a new version is available on nuget at startup ([81df0c3](https://github.com/DIGITALLNature/DigitallPower/commit/81df0c36385a482fa497054d4d832126dc781bf1))

# [1.8.0-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.7.1...v1.8.0-beta.1) (2023-04-06)


### Bug Fixes

* **dgt.power:** add version prefix to dgt.power.csproj to be able to replace it ([e45294f](https://github.com/DIGITALLNature/DigitallPower/commit/e45294fd735b3d82c5fcd0245b9edfb19220fd74))
* **dgt.power:** install semantic-version-release-update-file module ([f5f12c0](https://github.com/DIGITALLNature/DigitallPower/commit/f5f12c0eeaf0a582dbb4fc57b6347a6a2a384dd7))


### Features

* **dgt.power:** check if a new version is available on nuget at startup ([ba92822](https://github.com/DIGITALLNature/DigitallPower/commit/ba928222bf0b3fade2a72fcae827ea52cb0dbc87))
* **dgt.power:** skip version check on build agents ([7ae89af](https://github.com/DIGITALLNature/DigitallPower/commit/7ae89afe1fd1457d042af4e815080b22c3f7917f))
* **dgt.power:** update assembly version with semantic release ([8ecc02f](https://github.com/DIGITALLNature/DigitallPower/commit/8ecc02f108e57f6ca8b512ebcae7437f6cf979cc))

# [1.8.0-beta.4](https://github.com/DIGITALLNature/DigitallPower/compare/v1.8.0-beta.3...v1.8.0-beta.4) (2023-03-08)


### Features

* **dgt.power:** skip version check on build agents ([6e1c87d](https://github.com/DIGITALLNature/DigitallPower/commit/6e1c87d7015451c73498da21b1d1b4beff41c924))

# [1.8.0-beta.3](https://github.com/DIGITALLNature/DigitallPower/compare/v1.8.0-beta.2...v1.8.0-beta.3) (2023-03-08)


### Features

* **dgt.power:** check if a new version is available on nuget at startup ([fec7634](https://github.com/DIGITALLNature/DigitallPower/commit/fec7634ffc92077f13b703a31a23efc2d55bb234))

# [1.8.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v1.8.0-beta.1...v1.8.0-beta.2) (2023-03-07)


### Bug Fixes

* **dgt.power:** add version prefix to dgt.power.csproj to be able to replace it ([de33635](https://github.com/DIGITALLNature/DigitallPower/commit/de33635ec8b3171ac5a39b8da1e44eae17e9c3ea))

# [1.8.0-beta.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.7.0...v1.8.0-beta.1) (2023-03-07)


### Bug Fixes

* **dgt.power:** install semantic-version-release-update-file module ([153a89c](https://github.com/DIGITALLNature/DigitallPower/commit/)

### Features

* **dgt.power:** update assembly version with semantic release ([257bc99](https://github.com/DIGITALLNature/DigitallPower/commit/257bc99c51c4be1fc2db9ec4e4d2e944d9b7da04))


## [1.7.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.7.0...v1.7.1) (2023-03-24)

### Bug Fixes

* **codegeneration:** handled when bit options are named the same ([#42](https://github.com/DIGITALLNature/DigitallPower/issues/42)) ([0b6aa25](https://github.com/DIGITALLNature/DigitallPower/commit/0b6aa2587a1b78e954bf75c04684820fa17d0ac5))

# [1.7.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.6.3...v1.7.0) (2023-03-07)


### Features

* **maintenance:** tool to enable modern flows based on configuration ([#39](https://github.com/DIGITALLNature/DigitallPower/issues/39)) ([e425d70](https://github.com/DIGITALLNature/DigitallPower/commit/e425d7091bf857a131ff62ff7e2d2e562966a97e))

## [1.6.3](https://github.com/DIGITALLNature/DigitallPower/compare/v1.6.2...v1.6.3) (2023-03-06)


### Bug Fixes

* bugfixes for document template import and profile deletion on tool update ([#38](https://github.com/DIGITALLNature/DigitallPower/issues/38)) ([66d500a](https://github.com/DIGITALLNature/DigitallPower/commit/66d500ab6808710ebb231558c4984b7ebbc6d3e2))

## [1.6.2](https://github.com/DIGITALLNature/DigitallPower/compare/v1.6.1...v1.6.2) (2023-03-03)


### Bug Fixes

* **push:** corrects incorrect detection of whether the assembly needs to be updated ([fccc5d8](https://github.com/DIGITALLNature/DigitallPower/commit/fccc5d87140120c9e5ac1e37aa994ea72d8d0448))

## [1.6.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.6.0...v1.6.1) (2023-03-03)


### Bug Fixes

* **main:** escape Errormessage for pretty console print ([d2d0140](https://github.com/DIGITALLNature/DigitallPower/commit/d2d01401fdf22f6f767521ee8224c091ec339e29))

# [1.6.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.5.0...v1.6.0) (2023-03-03)


### Bug Fixes

* **push:** retrieve SolutionPrefix from Publisher ([2145410](https://github.com/DIGITALLNature/DigitallPower/commit/2145410bff8588d84233f94326a606c2ef5608bf))


### Features

* **push:** integration of dgt.registration ([d91c574](https://github.com/DIGITALLNature/DigitallPower/commit/d91c5745f248f27fe408a14d279b82ecf38db639))
* **push:** support for depdendant Plugins (.nupkg) ([54f6077](https://github.com/DIGITALLNature/DigitallPower/commit/54f6077a3747852f2df7df931f9457c566eec5e5))
* **push:** warn for new-prefix on missing solution for Packages and ignore prefix on search ([8988f5f](https://github.com/DIGITALLNature/DigitallPower/commit/8988f5f1af220659a8a725f1db1382873db9bac9))

# [1.5.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.4.1...v1.5.0) (2023-02-23)


### Bug Fixes

* make dgt.power buildable ([#33](https://github.com/DIGITALLNature/DigitallPower/issues/33)) ([c29e5f8](https://github.com/DIGITALLNature/DigitallPower/commit/c29e5f8ef6ed79a8b5a7ade98a928d4a0570a6e5))


### Features

* **maintenance:** add new command solution-version to increment the version of a solution ([#31](https://github.com/DIGITALLNature/DigitallPower/issues/31)) ([a7e61ed](https://github.com/DIGITALLNature/DigitallPower/commit/a7e61edf20dce6c8bc8db5060312fab42aa12901))

## [1.4.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.4.0...v1.4.1) (2023-02-22)


### Bug Fixes

* add needed usings for context no lock interceptor ([#30](https://github.com/DIGITALLNature/DigitallPower/issues/30)) ([9282cb1](https://github.com/DIGITALLNature/DigitallPower/commit/9282cb149af6bfa05b91508cb14b8c1d5410ff85))

# [1.4.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.3.0...v1.4.0) (2023-02-21)


### Features

* **codegeneration:** add NoLock interceptor on DataContext ([c0f49d2](https://github.com/DIGITALLNature/DigitallPower/commit/c0f49d21c3da566091ad18eac24b1213fadd8678))

# [1.3.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.2.0...v1.3.0) (2023-02-08)


### Features

* **maintenance:** export active carriers to json file ([#25](https://github.com/DIGITALLNature/DigitallPower/issues/25)) ([44f46a4](https://github.com/DIGITALLNature/DigitallPower/commit/44f46a451cef842ec17f1e01aa7ae39e3b4ed38a))

# [1.2.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.1.0...v1.2.0) (2023-02-08)


### Features

* **analyze:** Layer inspections in analyse module extended by top layer inspections and active layer inspections ([#22](https://github.com/DIGITALLNature/DigitallPower/issues/22)) ([3a37e19](https://github.com/DIGITALLNature/DigitallPower/commit/3a37e199f5709a8e0a4bbb1f60ab73b52f80e8ff))

# [1.1.0](https://github.com/DIGITALLNature/DigitallPower/compare/v1.0.1...v1.1.0) (2023-02-07)


### Bug Fixes

* show user friendly error messages when connection fails ([#17](https://github.com/DIGITALLNature/DigitallPower/issues/17)) ([31eaa4a](https://github.com/DIGITALLNature/DigitallPower/commit/31eaa4ac6c827b7184a400f096a16085e7e4c2fa))


### Features

* **maintenance:** protect calculated fields ([#16](https://github.com/DIGITALLNature/DigitallPower/issues/16)) ([17370f4](https://github.com/DIGITALLNature/DigitallPower/commit/17370f4a95299f21f985d6d4addde55774322731))

## [1.0.1](https://github.com/DIGITALLNature/DigitallPower/compare/v1.0.0...v1.0.1) (2023-02-06)


### Bug Fixes

* update company and copyright in csproj ([#15](https://github.com/DIGITALLNature/DigitallPower/issues/15)) ([98cdad3](https://github.com/DIGITALLNature/DigitallPower/commit/98cdad30f09ce1182c45fea931f75b4dc70c8ee4))

# 1.0.0 (2023-02-06)


### Features

* Fix classic mode for CodeGeneration ([824ff0a](https://github.com/DIGITALLNature/DigitallPower/commit/824ff0a71ae4098dce2adf718af0b480620498de))
* Release 1.0.0 ([7fb5b03](https://github.com/DIGITALLNature/DigitallPower/commit/7fb5b03011e0a57ec6f9a565fccf812d4dbae8c1))
* Supress linePragmas for t4 files ([#3](https://github.com/DIGITALLNature/DigitallPower/issues/3)) ([32eb4be](https://github.com/DIGITALLNature/DigitallPower/commit/32eb4beda5c75f4e7a4152c789f2001ed4d9352a))

# [1.0.0-beta.4](https://github.com/DIGITALLNature/DigitallPower/compare/v1.0.0-beta.3...v1.0.0-beta.4) (2023-02-06)


### Features

* add ToNamedEntityReference() for generated early bound models ([90f3bde](https://github.com/DIGITALLNature/DigitallPower/commit/90f3bde007be02a770f1297611a4ff1dd81491fe)), closes [#5](https://github.com/DIGITALLNature/DigitallPower/issues/5)

# [1.0.0-beta.3](https://github.com/DIGITALLNature/DigitallPower/compare/v1.0.0-beta.2...v1.0.0-beta.3) (2023-02-03)


### Bug Fixes

* remove markup errors, unused command arguments and make some arguments required ([f20d2f8](https://github.com/DIGITALLNature/DigitallPower/commit/f20d2f8980c8f0338df4312265861a27eb488777))
* repair invalid command examples ([8e37214](https://github.com/DIGITALLNature/DigitallPower/commit/8e3721483dba65cca40e20f211d1398890597c5f))


### Features

* add Attribute ExcludeFromCodeCoverage to Generated Models ([e40a9da](https://github.com/DIGITALLNature/DigitallPower/commit/e40a9da042a66e8d5f3e6d2c99e67319dd42262d))
* Supress linePragmas for t4 files ([#3](https://github.com/DIGITALLNature/DigitallPower/issues/3)) ([32eb4be](https://github.com/DIGITALLNature/DigitallPower/commit/32eb4beda5c75f4e7a4152c789f2001ed4d9352a))

# [1.0.0-beta.2](https://github.com/DIGITALLNature/DigitallPower/compare/v1.0.0-beta.1...v1.0.0-beta.2) (2023-02-03)


### Bug Fixes

* make profile name a required argument for profile select/delete to avoid null references ([6a9ac84](https://github.com/DIGITALLNature/DigitallPower/commit/6a9ac84c5ebee7ec211bfcc300a18b6e06955c6a))


### Features

* configfile or enviroment variables instead of profile to set connectionstring ([a06dab5](https://github.com/DIGITALLNature/DigitallPower/commit/a06dab558660c614b85ad17fa4a934308c21e6ea))

# 1.0.0-beta.1 (2023-02-03)


### Bug Fixes

* removewrong warning on non-windows for profile storage ([961518b](https://github.com/DIGITALLNature/DigitallPower/commit/961518b0a8760aaec89186aa0de6da559e10dda1))


### Features

* add commitlint ([d705eaf](https://github.com/DIGITALLNature/DigitallPower/commit/d705eaf6eb0cdd5f7f05f64eefca06b323bc3ba9))
* Fix classic mode for CodeGeneration ([824ff0a](https://github.com/DIGITALLNature/DigitallPower/commit/824ff0a71ae4098dce2adf718af0b480620498de))
