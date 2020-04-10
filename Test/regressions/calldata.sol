pragma solidity ^0.5.0;

contract Calldata {
    function updateWhitelists(address[] calldata _users, bool[] calldata _allows)
        external
    {   
        require(_users.length == _allows.length);
        for (uint i = 0 ; i < _users.length ; i++) {
            address _user = _users[i];
            bool _allow = _allows[i];
        }   
    }  
}
