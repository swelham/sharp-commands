# sharp-commands

## todo
* flags with typeof(T) value. initial special type handling
  * bool - accept 1, 0, true, false, t, f
  * arrays - see multi flag values
  * paths/files - accept path/file types (needs some thought)
  * ... more ideas
* flags with input validation. some inital ideas
  * required
  * range
  * item count (must have x number of elements for arrays type)
  * regex
  * ... more ideas
* flags with multiple values (ie ```cmd -a 1 2 3``` would result in array ```[1, 2, 3]```)
* support better ICommand interface for commands that are more like namespaces (have no actions but contain nested commands)
* add integration tests (windows/mono)
* docs
  * need code docs
  * need usage docs
  * need list of features

