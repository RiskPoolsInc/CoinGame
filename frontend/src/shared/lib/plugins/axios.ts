import axios from "axios";
import axiosRetry from 'axios-retry';

const instance = axios.create({ baseURL: process.env.VUE_APP_BACKEND_URL })

axiosRetry(
    instance,
    {
        retries: 4,
        retryDelay: (retryCount) => {
            return retryCount * 30000;
        }
    }
);

export default instance
