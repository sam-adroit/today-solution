export const paymentReducer = (state, action) => {
    switch(action.type) {
        case "loading":
            return {...state, loading: true}
        case "error":
            return {...state, loading: false, error: action.error}
        case "all-patient-payments":
            return {...state, loading: false, error: null, payments: action.payments}
        default:
            return state

    }
}