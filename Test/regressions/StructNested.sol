pragma solidity >=0.4.24 <0.6.0;

contract StructNested {

    struct Point {
        uint x;
        uint y;
    }
    
    struct Square {
        Point upperLeft;
        uint len;
    }

    Point point;
    Square square;

    function test() public {
        point.x = 1;
        point.y = 2;
        square.upperLeft = point;
        square.len = 10;
        
        assert (square.upperLeft.x == 1);
        assert (square.upperLeft.y == 2);
        assert (square.len == 10);
    }
}
