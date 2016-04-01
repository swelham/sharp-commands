# sharp-commands

## todo
* output formatter. move hard coded output format into it's own class/interface that would allow a custom formatter to be provided
* need to consider how best to handle the generic conversions. should only primitive types be allowed and force the caller to provide a callback that handles the conversion for complex types... or maybe support some form of custom converters to be specified in the CliApp.
* special flag value type handling
  * paths/files - accept path/file types (needs some thought)
  * ... more ideas
* flags with input validation. some inital ideas
  * required
  * range
  * item count (must have x number of elements for arrays type)
  * regex
  * ... more ideas
* support better ICommand interface for commands that are more like namespaces (have no actions but contain nested commands)
* add integration tests (windows/mono)
* docs
  * need code docs
  * need usage docs
  * need list of features
* example apps

