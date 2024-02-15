import { get, post } from "../api/api"

export const addPayment = async (body) => {
    try{
        let {entity} = await post(`payment/pay`,body)
        return entity
    }catch{

    }
}

export const getPaymentsByUserId = async (id, pagination) => {
    try{
        let {entity} = await get(`payment/GetPaymentsByPatientId/${id}?Skip=${pagination.currentPage}&Take=${pagination.itemPerPage}`)
        return entity
    }catch{

    }
}

export const getPaymentsByDateRange = async (id, pagination, dateRange) => {
    try{
        let {entity} = await get(`payment/GetPaymentsByDateRange/${id}?Skip=${pagination.currentPage}&Take=${pagination.itemPerPage}&StartDate=${dateRange.StartDate}&EndDate=${dateRange.EndDate}`)
        return entity
    }catch{

    }
}