import React, { useReducer } from 'react'
import { paymentReducer } from './paymentReducer'
import { PaymentContext } from '../context/Context'

function PaymentProvider({children}) {
    const initialState = {loading: false, error: null, isError: false, payments: []}
    const [state, dispatch] = useReducer(paymentReducer,initialState)
    return (
        <PaymentContext.Provider value={{state, dispatch}}>
            {children}
        </PaymentContext.Provider>
    
  )
}

export default PaymentProvider