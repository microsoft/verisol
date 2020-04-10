pragma solidity ^0.5.2;

contract A {
    struct Role {
        mapping (address => bool) bearer;
    }
	
    function add(Role storage role, address account) internal {
        require(!role.bearer[account]);
        role.bearer[account] = true;
    }
	
    Role private _minters;
	
    constructor () internal {
        _addMinter(msg.sender);
    }
	
    function _addMinter(address account) internal {
        add(_minters, account);
    }
}

contract B is A {
}

interface C {
}

contract D {
}

contract E is D {
    constructor (string memory name, string memory symbol) public {
    }
}

contract F is C, E {
    constructor (string memory name, string memory symbol) E(name, symbol) public {
    }
}

contract G is F, A {
}

contract H is F {
}

contract I is F {
}

contract J is H, I {
}

contract K is F, H, I, J, G, B
{

    constructor(string memory name, string memory symbol, uint256 initialAmount) F(name, symbol)
    public {
	assert(false);
    }   
}

