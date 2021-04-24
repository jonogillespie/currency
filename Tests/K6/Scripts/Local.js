import http from 'k6/http';
import {sleep, check, group} from 'k6';
import {Counter} from 'k6/metrics';

export const requests = new Counter('http_reqs')

export const options = {
    stages: [
        {target: 100, duration: '1m'}
    ],
    thresholds: {
        requests: ['count < 100']
    }
}

const BASE_URL = 'http://host.docker.internal:8080/v1'

export default function () {
    
   group('get currencies ', function() {
       const url = `${BASE_URL}/currencies`;
       const res = http.get(url);
       sleep(1);
       check(res, {
           'status is 200': resp => resp.status === 200
       })
   })

    group('get abbreviations', function() {
        const url = `${BASE_URL}/currencies/abbreviations`;
        const res = http.get(url);
        sleep(1);
        check(res, {
            'status is 200': resp => resp.status === 200
        })
    })
}