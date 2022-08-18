abstract class SetupStates{}

class InitialSetupState extends SetupStates{}

class LoadingSetupState extends SetupStates{}

class WaitingForImagesUploadState extends SetupStates{}

class CancelledUploadState extends SetupStates{}

class SuccessUploadState extends SetupStates{}

class ReadSuccessState extends SetupStates{}

class ReadErrorState extends SetupStates{}

class WriteSuccessState extends SetupStates{}

class WriteErrorState extends SetupStates{}