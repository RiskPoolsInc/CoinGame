import Analytics from 'analytics';
import googleTagManager from '@analytics/google-tag-manager';
import { App } from 'vue';

const analytics = Analytics({
    app: 'RiskPools',
    plugins: [
        googleTagManager({
            containerId: process.env.GOOGLE_TAG_MANAGER_ID as string,
        }),
    ],
});

export default {
    install: (app: App) => {
        app.provide('analytics', analytics);
    },
};
