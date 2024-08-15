import axios from "axios";
import axiosRetry from 'axios-retry';

const instance = axios.create({ baseURL: process.env.VUE_APP_BACKEND_URL })

axiosRetry(
    instance,
    {
        retries: 3,
        retryDelay: (retryCount) => {
            return retryCount * 5000;
        } 
    }
);

export default instance
