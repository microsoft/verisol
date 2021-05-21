# Celestial

Celestial as a framework evolved a lot over the last 2 years from its inception as a completely new language to a framework that allows annotating Solidity contracts with expressive specifications and automatically discharging them. However, even after repeated iterations, some elements from Celestial's previous versions still remain. (These differences and restrictions are **purely** syntactic). This document explains these deviations from vanilla Solidity and also its specification language.

## Language

A contract in Celestial follows the below structure:
```
contract <contract_name>
{
    <field_variables>;

    invariant <invariant_name>
    {
        <expression : bool>
    }

    spec <spec_function_name> (<args>)
    {
        <expression : bool>
    }

    function <function_name> (<args>)
        <public/private>
        <credit> <debit>
        pre <expression : bool>
        post <expression : bool>
        tx_reverts <expression : bool>
        modifies [<comma_separated_field_variable_names>]
        modifies_addresses [<comma_separated_addresses>]
        returns (<type> <variable_name>)
    {
        <contract_body_statements>;
        return;
    }
}
```

## Specification Language

Celestial's specification language contains the following constructs and annotations:
* `invariant`
* `spec` functions
* function specifications:
    * `credit`
    * `debit`
    * `pre`
    * `post`
    * `tx_reverts`
    * `modifies`
    * `modifies_addresses`
* `log` ghost variable
* quantifiers
* other macros

These elements are erased when generating Solidity. Note that all these constructs are 'pure'. None of the boolean expressions used in the specifications can modify the state. The following sections explain each of these constructs in detail.

### `invariant`
These are contract-level invariants. An `invariant`'s body is a single boolean expression over the contract field variables, including `balance`. All defined contract-invariants are conjugated and assumed in functions' pre-conditions are asserted in their post-conditions. We allow writing multiple `invariant` blocks for developer convenience and code readability.

### `spec` functions
These are pure functions whose bodies are just boolean expressions over their arguments. These functions allow for code reuse - repeating expressions can be wrapped in a `spec` function and called within `pre`, `post`, `tx_reverts` and other `spec` functions.

### function specifications

#### `credit` / `debit`
These are keywords that denote whether a contract function receives Ether, sends out Ether, both or neither. Functions that are not annotated with either `credit` or `debit` have a `new_balance == old_balance` clause added to its post-condition automatically. This check ensures that a contract does not send out Ether by accident. This forces developers to be cognizent of their contract's behviour, and explicitly add a `credit` or `debit` annotation to the function. Functions annotated with both `credit` and `debit` do not have any extra checks added automatically.

#### `pre`
This is the function's pre-condition, which is just a boolean expression over the function's arguments, field variables, `balance` and `spec` function invocations. For public functions, this is almost always trivial (`true`). For public functions in a contract that are never called by other functions in the same contract, for example, the pre-condiitons `address(this) != msg.sender` is sound. Private functions can have non-trivial pre-conditions. Of course, these pre-conditions have to be ensured before calling this private function, without which the verification would fail.

#### `post`
This is the function's post-condition, which is a boolean expression over the function's arguments, field variables, `log`, `balance`, the return variable, `spec` function invocations and also `new(<field_variable_name>)`. `new(<field_variable_name>)` denotes the updated value of the field variable (including `log` and `balance`) after the function is executed. The return variable is the name of the variable returned, denoted using Solidity's `returns (<type> <return_variable_name>)` clause.

#### `tx_reverts`
This is the function's revert clause which again is a boolean expression similar to `pre`. This specifies the conditions under which a function may revert (described in the paper in more detail).

#### `modifies`
This is a list of contract field variables that a function can modify. For all the field variables that are not in the `modifies` list, a `new_field_var == old_field_var` clause is added to the function's post-condition automatically. This ensures that a function cannot accidentally update a field variable. For functions that `emit` an event, the `log` variable should be added to the `modifies` clause explicitly. This prevents any unintended `emit`s caused due to that function.

#### `modifies_addresses`
This is a list of addresses that a function can modify. These are not address literals (of course, our blockchain model is unaware of the actual state of the Ethereum blockchain) but variables that represent contract addresses. For example, `Item.getPrice()` in the Overview example requires that `modifies_addresses[address(this)]` be specified so that the caller `SimpleMarket.buy()` is assured that its own state would not be modified on an invocation to `getPrice()` (due to a re-entrant call, for example).


### `log` ghost variable
The `log` is a specification-only variable (cannot be used in the contract body) that models the EVM log. An `emit(<event_name, <event_payloads>...)` in Celestial appends to the head of this log (a list) with a tuple `(<event_name>, <event_payloads>...)`. Post-conditions on `log` are written as follows: `new(log) == (<event_tuple1>)::(<event_tuple2>):: ... ::log`, where the head of the list is the last emitted event.

### quantifiers
Celestial also supports quantifiers in specification expressions as follows: `forall/exists (<type1> <var1>, <type2> <var2>, ...) (<boolean_expression_over_quantified_vars>)`. Note that excessive use of quantifiers can pollute Z3's VCs and may cause verification to break.

### macros
Celestial supports macros such as `sum_mapping` which sums all values contained in a `mapping (... => uint)`. It also supports checking membership of a key within a `mapping` using the `in` keyword (e.g. `... && (key in map_var) ...`). Soldity does not support these constructs, and hence are specification-only within Celestial. (Note, Solidity's `mapping()` type does not have the concept of a domain - all keys of that type are present in the mapping. However, we extend this mapping type in our F* model to allow writing more expressive specifications with respect to key membership in a mapping, say after adding or removing an element from the mapping).