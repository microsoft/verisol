pragma solidity >=0.4.24 <0.6.0;

library Lib {
    function add(uint _a, uint _b) public pure returns (uint r) {
        r = _a + _b;
    }
}

contract C {
    using Lib for uint;

     function foo() public {
        uint x = 1;
        uint y = 2;
        uint z = x.add(y);
        assert (z == 3);
    }
}

contract D {
   function add(uint x, uint y) public returns (uint r)  { return 1;}
}

/* 

pragma solidity >=0.4.16 <0.7.0;


// This is the same code as before, just without comments
library Set {
    struct Data { mapping(uint => bool) flags; }

    function insert(Data storage self, uint value)
        public
        returns (bool)
    {
        if (self.flags[value])
            return false; // already there
        self.flags[value] = true;
        return true;
    }

    function remove(Data storage self, uint value)
        public
        returns (bool)
    {
        if (!self.flags[value])
            return false; // not there
        self.flags[value] = false;
        return true;
    }

    function contains(Data storage self, uint value)
        public
        view
        returns (bool)
    {
        return self.flags[value];
    }
}


contract C {
    using Set for Set.Data; // this is the crucial change
    Set.Data knownValues;

    function register(uint value) public {
        // Here, all variables of type Set.Data have
        // corresponding member functions.
        // The following function call is identical to
        // `Set.insert(knownValues, value)`
        require(knownValues.insert(value));
    }
}


pragma solidity >=0.4.16 <0.7.0;

library Search {
    function indexOf(uint[] storage self, uint value)
        public
        view
        returns (uint)
    {
        for (uint i = 0; i < self.length; i++)
            if (self[i] == value) return i;
        return uint(-1);
    }
}

contract C {
    using Search for uint[];
    uint[] data;

    function append(uint value) public {
        data.push(value);
    }

    function replace(uint _old, uint _new) public {
        // This performs the library function call
        uint index = data.indexOf(_old);
        if (index == uint(-1))
            data.push(_new);
        else
            data[index] = _new;
    }
}

*/