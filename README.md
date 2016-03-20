# sharp-commands

## todo
* flag not found validation
* flags with typeof(T) value. initial special type handling
  * bool - accept 1, 0, true, false, t, f
  * arrays - accept comma separated value and split into array
  * paths/files - accept path/file types (needs some thought)
* support better ICommand interface for commands that are more like namespaces (have no actions but contain nested commands)
* add a more fluid api to build the CliApp instance
