import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class PaychecksSettingsService {
    async getPaychecksSettingsByProfileId(){
        const res = await api.get(`api/paychecksSettings`)
        AppState.paychecksSettings = res.data
        logger.log('[Paycheck Settings]', res.data)
    }
    // async getOne(id){
    //     const res = await api.get(`api/paycheckssettings/${id}`)
    //     AppState.pay = res.data
    //     logger.log('[Active Keep]', AppState.activeKeep)
    // }
    // async deleteKeep(id){
    //     const res = await api.delete(`api/paycheckssettings/${id}`)
    //     logger.log('Deleting Paycheck Settings')
    //     AppState.paychecksSettings = AppState.paychecks.filter(p => p.id != id)
    // }
    async createPaycheckSettings(psData){
        const res = await api.post('api/paycheckSettings', psData)
        logger.log("[res.data]", res.data)
        AppState.paychecksSettings.push(res.data)
    }
}

export const psService = new PaychecksSettingsService()