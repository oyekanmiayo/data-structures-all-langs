class Entry {
  constructor(key, value) {
    this.key = key;
    this.value = value;
    this.next = null;
  }
}

class Map {
  #PRIME = 31;

  #length = 29;

  #entries = new Array(this.#length);

  put(key, value) {
    let hash = this.getHashForKey(key);
    let newEntry = new Entry(key, value);

    // if there is no initial value at the beginning of the
    // bucket we just insert one
    if (!this.#entries[hash]) {
      this.#entries[hash] = newEntry;
      return;
    }

    let head = this.#entries[hash];
    let prev;

    while (head) {
      prev = head;
      if (prev.key === key) {
        throw new Error("Key already exists");
      }
      head = head.next;
    }

    prev.next = newEntry;
  }
  get(key) {
    let hash = this.getHashForKey(key);

    let head = this.#entries[hash];

    while (head) {
      if (head.key === key) {
        return head.value;
      }

      head = head.next;
    }

    return null;
  }
  delete(key) {
    let hash = this.getHashForKey(key);

    let head = this.#entries[hash];

    if (head?.key === key) {
      head = head.next;
      this.#entries[hash] = head;

      return;
    }

    let prev;

    while (head?.key !== key) {
      prev = head;
      head = head?.next;
    }

    prev.next = head?.next;

    return;
  }

  size() {
    let size = 0;
    for (let i = 0; i < this.#entries.length; i++) {
      let head = this.#entries[i];

      while (head) {
        size++;
        head = head.next;
      }
    }

    return size;
  }

  getHashForKey(key) {
    let stringifiedKey = key.toString();
    let sum = 0;

    for (let i = 0; i < stringifiedKey.length; i++) {
      sum += stringifiedKey.charCodeAt(i);
    }

    return (sum * this.#PRIME) % this.#entries.length;
  }
}

let m = new Map();

m.put("foes", 20);
m.put("lies", 53);

console.log(m.get("lies"));
