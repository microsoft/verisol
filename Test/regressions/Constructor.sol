pragma solidity ^0.4.24;

contract Foo {

    uint a;

    function Foo() public {
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
