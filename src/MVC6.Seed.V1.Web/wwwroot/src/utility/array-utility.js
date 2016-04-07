export function swap(fromArr, toArr, arg) {
    let item = remove(fromArr, arg);
    toArr.push(item);
}

export function remove(arr, arg) {
    let index = -1;
    if (typeof arg === 'function') {
        index = arr.findIndex(arg);
    } else {
        index = arr.indexOf(arg);
    }

    if (index < 0) {
        throw "Unable to find item";
    }

    return arr.splice(index, 1)[0];
}

export function except(first, second) {
    return first.filter(item => !second.includes(item));
}