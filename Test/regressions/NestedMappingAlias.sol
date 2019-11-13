pragma solidity >=0.4.24 <0.6.0;

// array variables should not alias with array params or local variables

contract A {

    mapping (address => uint256) private _balances;

    mapping (address => mapping (address => uint256)) private _allowances;

    constructor (address a, address b, address c) public 
    {
        _balances[a] = 44;
        require (b != c);
        _allowances[b][c] = 99;
        assert(_balances[a] == 44); //_balances and _allowances should not alias
        _allowances[c][b] = 111;
        assert(_allowances[b][c] == 99); //_allowances[a] and _allowances[b] should not alias
    }
}
