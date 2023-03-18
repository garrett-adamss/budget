import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class PaychecksService {
    async getPaychecksByProfileId(){
        const res = await api.get(`api/paychecks`)
        AppState.paychecks = res.data
        logger.log('[Paychecks]', res.data)
    }
    async getOne(id){
        const res = await api.get(`api/paychecks/${id}`)
        AppState.activePaycheck = res.data
        logger.log('[Active Keep]', AppState.activePaycheck)
    }
    async deleteKeep(id){
        const res = await api.delete(`api/paychecks/${id}`)
        logger.log('Deleting Keep')
        AppState.paychecks = AppState.paychecks.filter(p => p.id != id)
    }
    async createPaycheck(paycheckData){
        const res = await api.post('api/paychecks', paycheckData)
        logger.log("[res.data]", res.data)
        AppState.paychecks.push(res.data)
    }
}

export const paychecksService = new PaychecksService()