import { useContext } from 'react';
import SearchField from '../component/SearchField';
import { PatientContext } from '../context/Context';

function Home() {
  const {state} = useContext(PatientContext)
  return (
    <div>
        <SearchField/>
        {state?.patient &&
            <div className="patient-details">
                <h4>Firstname:</h4>
                <p>{state?.patient?.firstName}</p>
                <h4>Lastname:</h4>
                <p>{state?.patient?.lastName}</p>
                <h4>Patient Email:</h4>
                <p>{state?.patient?.email}</p>
                <h4>Patient Id:</h4>
                <p>{state?.patient?.email}</p>
                <h4>Current Balance:</h4>
                <p>{state?.patient?.balance}</p>
                <h4>Last Payment:</h4>
                <p>{state?.patient?.lastPayment}</p>
            </div>
        }
    </div>
  )
}

export default Home