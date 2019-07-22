pragma solidity >=0.4.24 <0.6.0;

contract EventTests {

    event EventFoo(address indexed addr, uint amount);
    event EventBar(address indexed addr);

    function foo(uint _a) public {
        emit EventFoo(msg.sender, _a);
    }

    function bar() public {
        emit EventBar(msg.sender);
    }
}
