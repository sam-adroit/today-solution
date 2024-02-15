import { get, post, remove, update } from "../api/api"

export const getPatientById = async (id) => {
    try{
        let {entity} = await get(`patient/getbyid/${+id}`)
        return entity
    }catch{

    }
    //return data.find(patient => patient.id === id ) || null
}
export const getPatientByEmail = async (email) => {
    try{
        let {entity} = await get(`patient/getbyemail?email=${email}`)
        return entity
    }catch{

    }
    //return data.find(patient => patient.email === email) || null
}

export const getPatientsByName = async (name, pageNumber, pageItem) => {
    try{
        let {entity} = await get(`patient/GetByName?name=${name}&Skip=${pageNumber}&Take=${pageItem}`)
        return entity
    }catch{

    }
    //return data.filter(patient => patient.name.toLowerCase().includes(name.toLowerCase()))
}

export const getAllPatients = async (pageNumber, pageItem) => {
    try{
        let {entity} = await get(`patient/GetAllPatients?Skip=${pageNumber}&Take=${pageItem}`)
        return entity
    }catch{

    }
}

export const addPatient = async (body) => {
    try{
        let {entity} = await post(`patient/create`,body)
        return entity
    }catch{

    }
}

export const deletePatient = async (id) => {
    try{
        let {entity} = await remove(id)
        return entity
    }catch{

    }
}

export const updatePatient = async (body) => {
    try{
        let {entity} = await update(`patient/update`,body)
        return entity
    }catch{

    }
}