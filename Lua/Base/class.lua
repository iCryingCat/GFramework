function Class(className)
    local class = _G[className]
    if class then
        error("already existed this class!!!")
    end
    class = {
        name = className
    }
    function class:new()
        local obj = {}
        obj.base = self
        self.__index = self
        setmetatable(obj, self)
        obj.name = self.name .. " clone"
        return obj
    end

    function class:extends(base)
        if not base or not _G[base.name] then
            error("try to extends from class that is not existed!!!")
        end
        local sub = {}
        sub.base = base
        self.__index = base
        setmetatable(sub, base)
        sub.name = "extends " .. base.name
        return sub
    end

    _G[className] = class
    return class
end

return Class
