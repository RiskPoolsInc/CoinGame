import { z } from 'zod'

const getEnvVar = (key: string) => {
    if (import.meta.env[key] === undefined) {
        throw new Error(`Env variable ${key} is required`)
    }
    return import.meta.env[key] || ''
}

const envVariables = z.object({
    VITE_API_ENDPOINT: z.string().url(),
})

envVariables.parse(import.meta.env)

declare global {
    interface ImportMetaEnv extends z.infer<typeof envVariables> {}
}

export const config = {
    API_ENDPOINT: getEnvVar('VITE_API_ENDPOINT'),
} as const
