pragma solidity >=0.4.24 <0.6.0;

contract Foo {

    uint a;

    constructor () public {
        a = 1;
    }

    function testConstructor() public {
        assert (a == 1);
    }

}

contract Bar {

    uint a;

    constructor () public {
        a = 2;
    }

    function testConstructorKeyword() public {
        assert (a == 2);
    }

}
