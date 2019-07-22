pragma solidity >=0.4.24 <0.6.0;

contract ArrayDynamicStorage {

    uint[] sa;

    function ArrayDynamicStorage() {
       assert (sa.length == 0);
    }
    function test() public {
        sa.push(10);
        sa.push(11);
        assert (sa[0] == 10);
        assert (sa[1] == 11);
        assert (sa.length == 2);
    }

}
