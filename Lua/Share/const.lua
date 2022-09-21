const = {}
local map = {}
local meta = {
    __newindex = function(t, k, v)
        if not map[k] then
            map[k] = v
        else
            error("try to assign a new value to a const var!!!")
        end
    end;
    __index = map
}

setmetatable(const, meta)
