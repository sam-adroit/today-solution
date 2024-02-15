import { BASE_URL } from "../utils/apiUrl"

export const get = async(path) => {
    let res = await fetch(BASE_URL + path, {
        headers: {
            "content-type": "application/json"
        }
    })
    let dat = await res.json();
    return dat;
}

export const post = async(path,body) => {
    const res = fetch(BASE_URL + path, {
        method: "post",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(body)
    })
    let data = await res.json()
    return data;
}

export const update = async(path, body) => {
    const res = fetch(BASE_URL + path, {
        method: "put",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(body)
    })
    let data = await res.json()
    return data;
}

export const remove = async(path, body) => {
    const res = fetch(BASE_URL + path, {
        method: "delete",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(body)
    })
    let data = await res.json()
    return data;
}